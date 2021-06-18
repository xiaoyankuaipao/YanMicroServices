using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Yan.Configuration.ConsulConfiguration
{
    /// <summary>
    /// Consul Key/Value 配置源
    /// </summary>
    public class ConsulConfigurationSource:IConfigurationSource
    {
        /// <summary>
        /// Consul 地址
        /// </summary>
        private readonly string _consulAddress;

        /// <summary>
        /// 数据中心
        /// </summary>
        private readonly string _dataCenter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulAddress"></param>
        /// <param name="dataCenter"></param>
        public ConsulConfigurationSource(string consulAddress, string dataCenter)
        {
            _consulAddress = consulAddress;
            _dataCenter = dataCenter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(_consulAddress, _dataCenter);
        }
    }
}
