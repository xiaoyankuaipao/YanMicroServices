using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Yan.Core
{
    /// <summary>
    /// Api请求 统一返回
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否响应成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="Data"></param>
        public ApiResult(bool success=true, int? code=null, string message=null, object data=null)
        {
            this.Success = success;
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }


    }
}
