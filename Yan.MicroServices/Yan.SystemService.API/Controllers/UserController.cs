using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.Core.Dtos;
using Yan.SystemService.API.Application.Commands;
using Yan.SystemService.API.Models;

namespace Yan.SystemService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult<ResultDto<UserInfo>> GetUserInfo()
        {
            string id = this.User.FindFirst("Id").Value;
            string userName = this.User.Identity.Name;
            string realName = this.User.FindFirst("RealName").Value;
            string email = this.User.FindFirst("Email").Value;

            UserInfo info = new UserInfo()
            {
                Id = id,
                UserName = userName,
                RealName = realName,
                Email = email
            };

            ResultDto<UserInfo> response = new ResultDto<UserInfo>()
            {
                State = 1,
                Data = info
            };

            return response;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<PageResultDto<UserDto>> GetUserList(int page, int rows)
        {
            var response = await _userService.GetUserListAsync(page, rows);
            return response;
        }

        /// <summary>
        /// 添加或者更新用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HandleResultDto> Post([FromBody] CreateUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return response;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<HandleResultDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<HandleResultDto> SetUserRole(int userId, int roleId)
        {
            throw new NotImplementedException();
        }



    }
}
