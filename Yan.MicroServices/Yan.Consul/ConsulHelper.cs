using Consul;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Consul
{
    public class ConsulHelper
    {

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="agentServiceName">代理服务名称</param>
        /// <param name="agentServiceAddress">代理服务地址</param>
        /// <param name="agentServicePort">代理服务端口号</param>
        /// <param name="agentServiceIntervalSeconds">检测间隔时间</param>
        /// <param name="agentServiceTimeOutSeconds">检测超时时间</param>
        /// <param name="tags">标记</param>
        /// <returns></returns>
        public static async Task<WriteResult> RegisterService(string consulAddress, string consulDataCenter,
            string agentServiceName, string agentServiceAddress, int agentServicePort,
            int agentServiceIntervalSeconds = 10, int agentServiceTimeOutSeconds = 5, string[] tags = null)
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                return await client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"AgentService_{agentServiceAddress}_{agentServicePort}",
                    Name = agentServiceName,
                    Address = agentServiceAddress,
                    Port = agentServicePort,
                    Check = new AgentServiceCheck
                    {
                        TCP = $"{agentServiceAddress}:{agentServicePort}",
                        Interval = TimeSpan.FromSeconds(agentServiceIntervalSeconds),
                        Timeout = TimeSpan.FromSeconds(agentServiceTimeOutSeconds),
                        DeregisterCriticalServiceAfter = TimeSpan.FromDays(14)
                    },
                    Tags = tags,
                    EnableTagOverride = true
                });
            }
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="agentServiceAddress">代理服务地址</param>
        /// <param name="agentServicePort">代理服务端口号</param>
        /// <returns></returns>
        public static async Task UnRegisterService(string consulAddress, string consulDataCenter,
            string agentServiceAddress, int agentServicePort)
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                await client.Agent.ServiceDeregister(
                    $"AgentService_{agentServiceAddress}_{agentServicePort}"); //服务停止时取消注册
            }
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="agentServiceName">代理服务名称</param>
        /// <returns></returns>
        public static async Task<List<AgentService>> GetServices(string consulAddress, string consulDataCenter,
            string agentServiceName = null)
        {
            return await GetServices(consulAddress, consulDataCenter, true, agentServiceName);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="passingOnly">是否过滤只通过的</param>
        /// <param name="agentServiceName">代理服务名称</param>
        /// <returns></returns>
        public static async Task<List<AgentService>> GetServices(string consulAddress, string consulDataCenter,
             bool passingOnly, string agentServiceName)
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                var healthQueryResult = await client.Health.Service(agentServiceName, null, passingOnly);
                var services = healthQueryResult.Response.Select(o => o.Service).ToList();
                return services;
            }
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="key">键值对的键</param>
        /// <param name="value">键值对的值</param>
        /// <returns></returns>
        public static async Task<WriteResult<bool>> PutKeyValuePair(string consulAddress, string consulDataCenter,
            string key, string value)
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                var putPair = new KVPair(key)
                {
                    Value = Encoding.UTF8.GetBytes(value)
                };
                return await client.KV.Put(putPair);
            }
        }

        /// <summary>
        /// 获取所有键值对
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="prefix">匹配前缀</param>
        /// <returns></returns>
        public static async Task<Dictionary<string, string>> GetKeyValuePairList(string consulAddress,
            string consulDataCenter, string prefix = "")
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                QueryResult<KVPair[]> kvPairList = await client.KV.List(prefix);
                return kvPairList.Response?.ToDictionary(o => o.Key,
                    o => o.Value == null ? null : Encoding.UTF8.GetString(o.Value, 0, o.Value.Length));
            }
        }

        /// <summary>
        /// 获取所有键值对
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <returns></returns>
        public static async Task<T> GetKeyValueObject<T>(string consulAddress,
            string consulDataCenter) where T : new()
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                T @object = new T();

                string prefix = string.Empty;
                object[] attributes = typeof(T).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length != 0)
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)attributes[0];
                    prefix = descriptionAttribute.Description;
                    if (!prefix.EndsWith("/"))
                    {
                        prefix += "/";
                    }
                }

                QueryResult<KVPair[]> kvPairList = await client.KV.List(prefix);
                if (kvPairList.Response == null)
                {
                    return default(T);
                }
                Dictionary<string, string> dictionaries = kvPairList.Response.ToDictionary(o => o.Key,
                    o => o.Value == null ? null : Encoding.UTF8.GetString(o.Value, 0, o.Value.Length));
                PropertyInfo[] propertyInfos = @object.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in propertyInfos)
                {
                    SetValue(@object, propertyInfo, dictionaries, prefix);
                  
                }

                return @object;
            }
        }

        private static void SetValue<T>(T @object, PropertyInfo propertyInfo, Dictionary<string, string> dictionaries, string prefix)
        {
            if (propertyInfo == null)
            {
                return;
            }

            if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType.IsValueType)
            {
                string key;
                object[] objects = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objects.Length != 0)
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objects[0];
                    key = prefix + descriptionAttribute.Description;
                }
                else
                {
                    key = prefix + propertyInfo.Name;
                }
                if (!dictionaries.ContainsKey(key))
                {
                    key = dictionaries.Keys.FirstOrDefault(o => o.EndsWith(key));
                    if (key == null)
                    {
                        return;
                    }
                }
                object value = dictionaries[key];
                object changeTypeValue = Convert.ChangeType(value, propertyInfo.PropertyType);
                propertyInfo.SetValue(@object, changeTypeValue);
            }
            else
            {
                object objectInstance = Activator.CreateInstance(propertyInfo.PropertyType);
                propertyInfo.SetValue(@object, objectInstance);
                object[] attributes = objectInstance.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length != 0)
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)attributes[0];
                    prefix += descriptionAttribute.Description;
                    if (!prefix.EndsWith("/"))
                    {
                        prefix += "/";
                    }
                }
                PropertyInfo[] propertyInfos = propertyInfo.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo2 in propertyInfos)
                {
                    SetValue(objectInstance, propertyInfo2, dictionaries, prefix);
                }
            }
        }

        /// <summary>
        /// 根据键值对的键获取键值对的值
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="consulDataCenter">Consul数据中心</param>
        /// <param name="key">键值对的键</param>
        /// <returns></returns>
        public static async Task<string> GetKeyValuePair(string consulAddress, string consulDataCenter,
            string key)
        {
            using (var client = new ConsulClient(configuration =>
            {
                configuration.Address = new Uri(consulAddress);
                configuration.Datacenter = consulDataCenter;
            }))
            {
                QueryResult<KVPair> kvPairResult = await client.KV.Get(key);

                if (kvPairResult.Response?.Value == null)
                {
                    return null;
                }

                return Encoding.UTF8.GetString(kvPairResult.Response.Value, 0, kvPairResult.Response.Value.Length);
            }
        }
    }
}
