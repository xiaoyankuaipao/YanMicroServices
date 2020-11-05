using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yan.ArticleService.Infrastructure;

namespace Yan.ArticleService.API
{
    /// <summary>
    /// 
    /// </summary>
    public class HasApiPathRequirement : IAuthorizationRequirement
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class HasApiPathHandler : AuthorizationHandler<HasApiPathRequirement>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ArticleContext _articleContext;
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleContext"></param>
        /// <param name="httpContextAccessor"></param>
        public HasApiPathHandler(ArticleContext articleContext, IHttpContextAccessor httpContextAccessor)
        {
            _articleContext = articleContext;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasApiPathRequirement requirement)
        {
            if(context.Resource is Endpoint endpoint)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            if (context.User.Identity.IsAuthenticated)
            {
                string path = _httpContextAccessor.HttpContext.Request.Path;
                string userId = context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                //在这里可以到数据库里面查询当前用户是否具有该接口的调用权限
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class OrHasAPiPathHandler : AuthorizationHandler<HasApiPathRequirement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasApiPathRequirement requirement)
        {
            //当需要多个处理程序的时候，可以使用context.HasSucceeded 和context.HasFailed 判断之前的判断状态，
            if (context.HasSucceeded)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }


}
