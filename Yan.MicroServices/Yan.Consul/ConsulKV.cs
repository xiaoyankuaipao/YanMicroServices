using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.Consul
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsulKV
    {
        /// <summary>
        /// 
        /// </summary>
        public RabbitMQ RabbitMQ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Authority { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RabbitMQ
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExchangeName { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public string YanArticleDb { get; set; }
    }

}
