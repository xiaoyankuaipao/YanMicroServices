using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper.DapperWrapper
{ 
    /// <summary>
    /// 数据库上下文配置选项
    /// </summary>
    public class DapperDbContextOptions : IOptions<DapperDbContextOptions>
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DapperDbContextOptions IOptions<DapperDbContextOptions>.Value { get { return this; } }

    }
}
