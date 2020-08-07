using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Core.Dtos
{
    /// <summary>
    /// 查询输出
    /// </summary>
    public class ResultDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
    }
}
