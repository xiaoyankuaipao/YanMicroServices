using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Consul;
using Microsoft.Extensions.Configuration;

namespace Yan.Configuration.ConsulConfiguration
{
    /// <summary>
    /// Consul Key/Value 配置提供程序
    /// </summary>
    public class ConsulConfigurationProvider:ConfigurationProvider
    {
        /// <summary>
        /// Consul 地址
        /// </summary>
        private readonly string _consulAddress;

        /// <summary>
        ///  数据中心
        /// </summary>
        private readonly string _dataCenter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulAddress"></param>
        /// <param name="dataCenter"></param>
        public ConsulConfigurationProvider(string consulAddress, string dataCenter)
        {
            _consulAddress = consulAddress;
            _dataCenter = dataCenter;
        }

        /// <summary>
        /// 配置 load
        /// </summary>
        public override void Load()
        {
            using var client = new ConsulClient(con =>
            {
                con.Address = new Uri(_consulAddress);
                con.Datacenter = _dataCenter;
            });

            var data = client.KV.List("").Result.Response?.ToDictionary(x => x.Key,
                x => x.Value == null ? null : Encoding.UTF8.GetString(x.Value, 0, x.Value.Length));

            if (data != null)
            {
                foreach (var a in data)
                {
                    Data.Add(new KeyValuePair<string, string>(a.Key.Replace('/', ':'), a.Value));
                }
            }
        }
    }
}
