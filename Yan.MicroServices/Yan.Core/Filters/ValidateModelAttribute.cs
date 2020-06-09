using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yan.Core.Filters
{
    /// <summary>
    /// 输入模型验证过滤器
    /// </summary>
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ValidationFailedResult : ObjectResult
    {

        public ValidationFailedResult(ModelStateDictionary modelState)
              : base(new ValidationFailedResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        public ValidationFailedResult(string validationSummarye)
              : base(new ValidationFailedResultModel(validationSummarye))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValidationFailedResultModel : ApiResult
    {
        public ValidationFailedResultModel(ModelStateDictionary modelState)
        {
            Code = 422;
            Message = "参数不合法";
            Data = modelState.Keys
                        .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();
            Success = false;
        }

        public ValidationFailedResultModel(string validationSummary)
        {
            Code = 422;
            Message = "参数不合法";
            Data = validationSummary;
            Success = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValidationError
    { 
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
