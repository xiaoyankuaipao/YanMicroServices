using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.ArticleService.API.Application.Commands;

namespace Yan.ArticleService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ArticleCategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// create Article Category
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> CreateArticleCategory([FromBody]CreateArticleCategoryCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }
    }
}