using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Yan.Configuration.MysqlConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlConfigurationProvider:ConfigurationProvider
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
        public MysqlConfigurationProvider(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings)
        {
            _setup = setup;
            _initialSettings = initialSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            var builder=new DbContextOptionsBuilder<ApplicationSettingContext>();
            _setup(builder);
            using var dbContext = new ApplicationSettingContext(builder.Options);

            dbContext.Database.EnsureCreated();
            Data = dbContext.Settings.Any()
                ? dbContext.Settings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase)
                : Initialize(dbContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private IDictionary<string, string> Initialize(ApplicationSettingContext dbContext)
        {
            foreach (var item in _initialSettings)
            {
                dbContext.Settings.Add(new ApplicationSetting(item.Key, item.Value));
            }

            dbContext.SaveChanges();

            return _initialSettings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
