using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.Core.Dtos;
using Yan.SystemService.API.Application.Commands;
using Yan.SystemService.API.Application.Queries;
using Yan.SystemService.API.Models;

namespace Yan.SystemService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public RoleController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 根据角色Id获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ResultDto<RoleDto>>> GetRoleById(string id)
        {
            var response = await _mediator.Send(new RoleByIdQuery { RoleId = id }, HttpContext.RequestAborted);
            return response;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<PageResultDto<RoleDto>>> GetRoleList()
        {
            var response = await _mediator.Send(new RoleListQuery(), HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 添加或者修改角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<HandleResultDto>> Post([FromBody] CreateRoleCommand cmd)
        {
            var response = await _mediator.Send(cmd, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 根据Id删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<HandleResultDto>> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand { RoleId = id }, HttpContext.RequestAborted);
            return response;
        }

    }
}
