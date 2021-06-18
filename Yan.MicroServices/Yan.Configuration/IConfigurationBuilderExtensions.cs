using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yan.Configuration.ConsulConfiguration;
using Yan.Configuration.MysqlConfiguration;

namespace Yan.Configuration
{
    /// <summary>
    /// IConfigurationBuilder 扩展
    /// </summary>
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// IConfigurationBuilder 扩展方法：添加 Consul Key/Value 配置源
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="consulAddress"></param>
        /// <param name="dataCenter"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsulConf(this IConfigurationBuilder builder, string consulAddress, string dataCenter)
        {
            var source = new ConsulConfigurationSource(consulAddress, dataCenter);
            builder.Add(source);
            return builder;
        }


        /// <summary>
        /// IConfigurationBuilder 扩展方法：添加 Mysql 配置源
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString"></param>
        /// <param name="initialSettings"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddMysqlConf(this IConfigurationBuilder builder, string connectionString,
            IDictionary<string, string> initialSettings = null)
        {
            var source = new MysqlConfigurationSource(optionsBuilder => optionsBuilder.UseMySql(connectionString),
                initialSettings);
            builder.Add(source);
            return builder;
        }
    }
}
