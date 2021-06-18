using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Yan.Configuration.MysqlConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlConfigurationSource:IConfigurationSource
    { 
        /// <summary>
        /// 
        /// </summary>
        private readonly Action<DbContextOptionsBuilder> _setup;

        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, string> _initialSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="initialSettings"></param>
        public MysqlConfigurationSource(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings)
        {
            _setup = setup;
            _initialSettings = initialSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MysqlConfigurationProvider(_setup, _initialSettings);
        }
    }
}
