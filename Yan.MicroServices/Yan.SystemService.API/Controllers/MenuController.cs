using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.Core.Dtos;
using Yan.SystemService.API.Application.Commands;
using Yan.SystemService.API.Application.Queries;
using Yan.SystemService.API.Models;

namespace Yan.SystemService.API.Controllers
{
    /// <summary>
    /// s
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public MenuController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        /// <summary>
        /// 根据Id获取菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDto<MenuDto>>> Get(string id)
        {
            var response = await _mediator.Send(new MenuByIdQuery { MenuId = id }, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 根据Id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<HandleResultDto>> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteMenuCommand { MenuId = id }, HttpContext.RequestAborted);

            return response;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<HandleResultDto>> Post([FromBody] CreateMenuCommand cmd)
        {
            var response = await _mediator.Send(cmd, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<HandleResultDto>> Put([FromBody] CreateMenuCommand cmd)
        {
            var response = await _mediator.Send(cmd, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<ResultDto<List<MenuTreeDto>>>> GetMenuTree()
        {
            var response = await _mediator.Send(new MenuTreeQuery(), HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 获取菜单树combox Tree
        /// </summary>
        [HttpGet("[action]")]
        public async Task<ActionResult<ResultDto<List<ComboxTreeDto>>>> GetMenuComboxTree()
        {
            var response = await _mediator.Send(new MenuComboxTreeQuery(), HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 根据角色获取该角色具有的菜单树列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{roleId}")]
        public async Task<ActionResult<ResultDto<List<MenuTreeDto>>>> GetMenuTreeByRoleId(string roleId)
        {
            var response = await _mediator.Send(new MenuTreeByRoleIdQuery { RoleId = roleId }, HttpContext.RequestAborted);
            return response;
        }

    }
}
