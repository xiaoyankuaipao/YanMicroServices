using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Core.Dtos
{
    /// <summary>
    /// 分页查询输出
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ResultPage<T> Result { get; set; }
    }
}
