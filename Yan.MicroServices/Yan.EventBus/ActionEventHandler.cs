using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.EventBus
{
    /// <summary>
    /// 支持Action的事件处理器
    /// </summary>
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData> where TEventData : IEventData
    {
        /// <summary>
        /// 定义Action的引用，并通过构造函数传参初始化
        /// </summary>
        public Action<TEventData> Action { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// 调用具体的Action来处理事件逻辑
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}
