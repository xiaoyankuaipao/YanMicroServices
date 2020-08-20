using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yan.EventBus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus
    {
        /// <summary>
        /// 定义锁对象
        /// </summary>
        private static readonly object LockObj = new object();

        /// <summary>
        /// 
        /// </summary>
        public static EventBus _eventBus = null;

        /// <summary>
        /// IOC 容器
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// 事件源-事件处理安全集合
        /// </summary>
        public readonly ConcurrentDictionary<Type, List<Type>> _eventAndHandlerMapping;

        /// <summary>
        /// 
        /// </summary>
        public EventBus()
        {
            IocContainer = new WindsorContainer();
            _eventAndHandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        /// <summary>
        /// 事件总线单例
        /// </summary>
        public static EventBus Instance
        {
            get
            {
                return _eventBus ?? (_eventBus = new EventBus());
            }
        }

        #region Register
        /// <summary>
        /// 手动绑定事件源与事件处理
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventHandler"></param>
        public void Register<TEventData>(IEventHandler eventHandler) where TEventData:IEventData
        {
            Register(typeof(TEventData), eventHandler.GetType());
        }

        /// <summary>
        /// 手动绑定事件源与事件处理
        /// </summary>
        /// <param name="eventDataType"></param>
        /// <param name="handlerType"></param>
        private void Register(Type eventDataType, Type handlerType)
        {
            //注册IEventHandler<T> 到IOC 容器
            var handlerInterface = handlerType.GetInterface("IEventHandler`1");
            if (!IocContainer.Kernel.HasComponent(handlerInterface))
            {
                IocContainer.Register(Component.For(handlerInterface, handlerType));
            }

            //添加到 事件源-事件处理安全集合
            lock (LockObj)
            {
                if (!_eventAndHandlerMapping.ContainsKey(eventDataType))
                {
                    var lstHandlerType = new List<Type>() { handlerType };
                    _eventAndHandlerMapping.TryAdd(eventDataType, lstHandlerType);
                }
                else
                {
                    var lstHandlerType = _eventAndHandlerMapping[eventDataType];
                    if (!lstHandlerType.Contains(handlerType))
                    {
                        lstHandlerType.Add(handlerType);
                        _eventAndHandlerMapping[eventDataType] = lstHandlerType;
                    }

                }
            }
        }

        /// <summary>
        /// 注册Action事件处理器
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="action"></param>
        public void Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            //1、构造ActionEventHandler
            var actionHandler = new ActionEventHandler<TEventData>(action);

            //2、将ActionEventHandler的实例注入到Ioc容器
            IocContainer.Register(Component.For<IEventHandler<TEventData>>().UsingFactoryMethod(() => actionHandler).LifestyleSingleton());

            //3、注册到事件总线
            Register<TEventData>(actionHandler);
        }

        #endregion

        /// <summary>
        /// 提供入口支持注册其他程序集中实现的IEventHandler
        /// </summary>
        /// <param name="assembly"></param>
        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            //1、将IEventHandler注册到Ioc容器
            IocContainer.Register(Classes.FromAssembly(assembly).BasedOn(typeof(IEventHandler<>)).WithService.Base());

            //2、从Ioc容器中获取所注册的所有IEventHandler
            var handlers = IocContainer.Kernel.GetAssignableHandlers(typeof(IEventHandler));
            foreach (var handler in handlers)
            {
                //循环遍历所有的IEventHandler<T>
                var interfaces = handler.ComponentModel.Implementation.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).IsAssignableFrom(@interface))
                    {
                        continue;
                    }
                    //获取泛型参数类型
                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        //注册到事件源与事件处理的映射字典中
                        Register(genericArgs[0], handler.ComponentModel.Implementation);
                    }
                }
            }
        }

        #region UnRegister
        /// <summary>
        /// 手动解除事件源与事件处理的绑定
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventHadnler"></param>
        public void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData
        {
            lock (LockObj)
            {
                if (_eventAndHandlerMapping.ContainsKey(typeof(TEventData)))
                {
                    List<Type> handlerTypes = _eventAndHandlerMapping[typeof(TEventData)];
                    if (handlerTypes.Contains(handlerType))
                    {
                        handlerTypes.Remove(handlerType);
                        _eventAndHandlerMapping[typeof(TEventData)] = handlerTypes;
                    }
                }
            }
        }

        /// <summary>
        /// 手动解除指定事件源上绑定的所有事件
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        public void UnRegister<TEventData>() where TEventData : IEventData
        {
            lock (LockObj)
            {
                if (_eventAndHandlerMapping.ContainsKey(typeof(TEventData)))
                {
                    _eventAndHandlerMapping.Keys.Remove(typeof(TEventData));
                }
            }
        }

        #endregion

        #region Trigger

        /// <summary>
        /// 根据事件源触发绑定的事件处理
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventData"></param>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            if (_eventAndHandlerMapping.ContainsKey(eventData.GetType()))
            {
                //获取所有映射的EventHandler
                List<Type> handlerTypes = _eventAndHandlerMapping[eventData.GetType()];
                if (handlerTypes.Count > 0)
                {
                    foreach (var handlerType in handlerTypes)
                    {
                        //从Ioc容器中获取所有的实例
                        var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                        var eventHandlers = IocContainer.ResolveAll(handlerInterface);

                        //循环遍历，仅当解析的实例类型与映射字典中事件处理类型一致时，才触发事件
                        foreach (var eventHandler in eventHandlers)
                        {
                            if (eventHandler.GetType() == handlerType)
                            {
                                var handler = eventHandler as IEventHandler<TEventData>;
                                handler?.HandleEvent(eventData);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 异步触发
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="evetData"></param>
        /// <returns></returns>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return Task.Run(() => Trigger<TEventData>(eventData));
        }

        /// <summary>
        /// 触发指定的EventHandler
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventData"></param>
        /// <param name="eventHandlerType"></param>
        public void Trigger<TEventData>(TEventData eventData, Type eventHandlerType) where TEventData:IEventData
        {
            //获取类型实现的泛型接口
            var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");

            var eventHandlers = IocContainer.ResolveAll(handlerInterface);

            //循环遍历，仅当解析的实例类型与映射字典中事件处理类型一致时，才触发事件
            foreach (var eventHandler in eventHandlers)
            {
                if (eventHandler.GetType() == eventHandlerType)
                {
                    var handler = eventHandler as IEventHandler<TEventData>;
                    handler?.HandleEvent(eventData);
                }
            }
        }

        public Task TriggerAsync<TEventData>(TEventData eventData, Type eventHandlerType) where TEventData : IEventData
        {
            return Task.Run(() => Trigger(eventData, eventHandlerType));
        }
        #endregion
    }
}
