using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.Core.Dtos;
using Yan.SystemService.API.Application.Queries;
using Yan.SystemService.API.Models;

namespace Yan.SystemService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public PermissionController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 获取当前用户具有的菜单树列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<ResultDto<List<MenuTreeDto>>>> GetUserPermissionMenuTree()
        {
            var userId = this.User.FindFirst("Id").Value;
            var response = await _mediator.Send(new UserPermissionMenuTreeQuery { UserId = userId });
            return response;
        }

        /// <summary>
        /// 获取当前用户具有的按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<ResultDto<string>>> GetUserBtnPermission()
        {
            var userId = this.User.FindFirst("Id").Value;
            var response = await _mediator.Send(new UserBtnPermissionQuery { UserId = userId }, HttpContext.RequestAborted);
            return response;
        }
    }
}
