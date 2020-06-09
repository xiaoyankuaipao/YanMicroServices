using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Core.Filters
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class CustomExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IKnownException knownException = context.Exception as IKnownException;
            if (knownException == null) //这是系统异常
            {
                knownException = KnownException.Unknown;
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //系统异常处理，可以写日志等操作，将真正的异常信息保存下来，


            }
            else //这是业务异常
            {
                knownException = KnownException.FromKnownException(knownException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }

            context.ExceptionHandled = true;
            context.Result = new CustomExceptionResult(knownException.ErrorCode, knownException.Message);
        }
    }

    public class CustomExceptionResultModel : ApiResult
    {
        public CustomExceptionResultModel(int? code, string message)
        {
            Code = code;
            Message = message;
            Success = false;
        }
    }

    public class CustomExceptionResult : ObjectResult
    {
        public CustomExceptionResult(int? code, string message)
           : base(new CustomExceptionResultModel(code, message))
        {
            StatusCode = code;
        }
    }

    //已知的业务异常
    public interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }
    }

    public class KnownException : IKnownException
    {
        public string Message { get;  set; }

        public int ErrorCode { get; private set; }

        public readonly static IKnownException Unknown = new KnownException
        {
            Message = "未知错误",
            ErrorCode = 9999
        };

        public static IKnownException FromKnownException(IKnownException exception)
        {
            return new KnownException
            {
                Message = exception.Message,
                ErrorCode = exception.ErrorCode,
            };
        }
    }

    //当已知的业务异常发生是，throw 该异常
    public class MyServerException : Exception, IKnownException
    {
        public MyServerException(string message, int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; private set; }
    }
}
