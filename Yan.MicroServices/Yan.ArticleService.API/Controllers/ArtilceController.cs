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

namespace Yan.ArticleService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/articlemanage/[controller]/[action]")]
    [ApiController]
    public class ArtilceController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ArtilceController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// add article
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<HandleResultDto> AddArticle([FromBody] CreateArticleCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// edit article
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<HandleResultDto> EditArticle([FromBody] UpdateArticleCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// like article
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<HandleResultDto> DeleteArticle(int id)
        {
            return await _mediator.Send(new DeleteArticleCommand { ArticleId = id }, HttpContext.RequestAborted); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<HandleResultDto> LikeThisArticle(int articleId)
        {
            return await _mediator.Send(new LikeArticleCommand { ArticleId = articleId }, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultDto<List<CategoryArticleCount>>> GetArticleStaticCountByCategory()
        {
            return await _mediator.Send(new QueryCategoryArticleCountCommand(), HttpContext.RequestAborted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResultDto<ArticleListDto>> GetArticlePageByCategory(int categoryId, int page, int rows)
        {
            return await _mediator.Send(new QueryArticlePageByCategoryCommand { CategoryId = categoryId, Page = page, Rows = rows }, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<ResultDto<ArticleOutputDto>>> GetArticleById(int articleId)
        {
            return await _mediator.Send(new QueryArticleCommand { ArticleId = articleId }, HttpContext.RequestAborted);
        }

    }
}