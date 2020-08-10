using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.ArticleService.API.Application.Commands;
using Yan.ArticleService.API.Application.Queries;
using Yan.ArticleService.API.Models;
using Yan.Core.Dtos;
using Yan.Core.Filters;

namespace Yan.ArticleService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/articlemanage/[controller]")]
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
        [Authorize]
        [HttpPost]
        public async Task<HandleResultDto> Post([FromBody]CreateArticleCategoryCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// delete articlecategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<HandleResultDto> Delete(int id)
        {
            return await _mediator.Send(new DeleteArticleCategoryCommand { CategoryId = id }, HttpContext.RequestAborted);
        }

        /// <summary>
        /// update artilcecategory
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<HandleResultDto> Put([FromBody]UpdateArticleCategoryCommand cmd)
        {
            return await _mediator.Send(cmd,HttpContext.RequestAborted);
        }

        /// <summary>
        /// get articlecategory list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<PageResultDto<ArticleCategoryDto>> GetArticleCategoryList()
        {
            var response = await _mediator.Send(new ArticleCategoryListQuery(), HttpContext.RequestAborted);
            return response;
        }
    }
}