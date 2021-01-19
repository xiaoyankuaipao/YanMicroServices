using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.DapperCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDapperFactory
    {
        DapperClient CreateClient(string name);
    }
}
