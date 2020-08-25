using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
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
    [Route("api/articlemanage/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public MessageController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<HandleResultDto> AddMessage([FromBody] AddMessageCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<PageResultDto<MessageOutputDto>> GetMessagePage(int page, int size)
        {
            return await _mediator.Send(new MessagePageQuery { Page = page, Size = size }, HttpContext.RequestAborted);
        }
    }
}
