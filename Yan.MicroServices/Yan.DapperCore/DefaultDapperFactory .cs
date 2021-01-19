using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yan.DapperCore
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDapperFactory : IDapperFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceProvider _service;

        /// <summary>
        /// 
        /// </summary>
        private readonly IOptionsMonitor<DapperFactoryOptions> _optionsMonitor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="optionsMonitor"></param>
        public DefaultDapperFactory(IServiceProvider service, IOptionsMonitor<DapperFactoryOptions> optionsMonitor)
        {
            _service = service;
            _optionsMonitor = optionsMonitor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DapperClient CreateClient(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var client = new DapperClient(new ConnectionConfig { });

            var option = _optionsMonitor.Get(name).DapperActions.FirstOrDefault();
            if (option != null)
            {
                option(client.CurrentConnectionConfig);
            }
            else
            {
                throw new ArgumentNullException(nameof(option));
            }

            return client;
        }
    }
}
