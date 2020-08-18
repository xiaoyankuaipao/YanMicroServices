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

namespace Yan.SystemService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleMenuController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public RoleMenuController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        /// <summary>
        /// 保存角色具有的菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost("{roleId}")]
        public async Task<ActionResult<HandleResultDto>> Post([FromBody] string[] menuIds, string roleId)
        {
            var response = await _mediator.Send(new SaveRoleMenuCommand { MenuIds = menuIds, RoleId = roleId }, HttpContext.RequestAborted);

            return response;
        }
    }
}
