using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.DapperCore
{
    /// <summary>
    /// 
    /// </summary>
    public class DapperFactoryOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<Action<ConnectionConfig>> DapperActions { get; } = new List<Action<ConnectionConfig>>();
    }
}
