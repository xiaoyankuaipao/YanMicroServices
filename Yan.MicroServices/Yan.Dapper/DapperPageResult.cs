using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper
{
    /// <summary>
    /// Dapper 分页查询返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DapperPageResult<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> Data { get; set; }
    }
}
