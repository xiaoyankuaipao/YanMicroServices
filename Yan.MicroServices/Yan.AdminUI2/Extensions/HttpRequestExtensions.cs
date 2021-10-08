using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Yan.AdminUI2.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjax(this HttpRequest request)
        {
            if (request.Headers.ContainsKey("axios") && request.Headers["axios"] == "true")
            {
                return true;
            }

            return false;
        }

    }
}
