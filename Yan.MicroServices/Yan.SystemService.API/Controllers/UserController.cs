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
    /// 
    /// </summary>
    [Route("api/systemmanageservice/[controller]")]
    [ApiController]
    [Authorize]
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
            string id = this.User.FindFirst("id").Value;
            //string userName = this.User.Identity.Name;
            string userName = this.User.FindFirst("name").Value;
            string realName = this.User.FindFirst("realname").Value;
            string email = this.User.FindFirst("email").Value;

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
        /// 获取用户信息和菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ResultDto<UserInfoAndMenuDto>> GetUserInfoAndMenu()
        {
            UserInfo info = new UserInfo()
            {
                Id = User.FindFirst("id").Value,
                UserName = User.FindFirst("name").Value,
                RealName = User.FindFirst("realname").Value,
                Email = User.FindFirst("email").Value
            };

            var userId = this.User.FindFirst("id").Value;
            var menus = await _mediator.Send(new UserPermissionMenuTreeQuery { UserId = userId });

            ResultDto<UserInfoAndMenuDto> response = new ResultDto<UserInfoAndMenuDto>
            {
                State = menus.State,
                Message = menus.Message,
                Data = new UserInfoAndMenuDto()
                {
                    UserInfo = info,
                    MenuTreeDtos = menus.Data
                }
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
            var response = await _mediator.Send(new UserListQuery { Page = page, Rows = rows }, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 添加或者更新用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HandleResultDto> Post([FromBody] CreateUserCommand cmd)
        {
            var response = await _mediator.Send(cmd, HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<HandleResultDto> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { UserId = id } ,HttpContext.RequestAborted);
            return response;
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<HandleResultDto> SetUserRole(string userId, string roleId)
        {
            var response = await _mediator.Send(new UpdateUserRoleCommand { UserId = userId, RoleId = roleId }, HttpContext.RequestAborted);
            return response;
        }



    }
}
