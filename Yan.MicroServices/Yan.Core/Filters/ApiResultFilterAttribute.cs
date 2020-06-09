using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Core.Filters
{
    /// <summary>
    /// apiresult 过滤器，统一api返回
    /// </summary>
    public class ApiResultFilterAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ValidationFailedResult)
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = objectResult;
            }
            else
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = new OkObjectResult(new ApiResult(true, code: 200, data: objectResult.Value));
            }
        }
    }
}
