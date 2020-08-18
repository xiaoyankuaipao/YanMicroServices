using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.Dapper;
using Yan.SystemService.API.Models;

namespace Yan.SystemService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPermissionMenuTreeQuery:IRequest<ResultDto<List<MenuTreeDto>>>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserPermissionMenuTreeQueryHandler : IRequestHandler<UserPermissionMenuTreeQuery, ResultDto<List<MenuTreeDto>>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public UserPermissionMenuTreeQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<MenuTreeDto>>> Handle(UserPermissionMenuTreeQuery request, CancellationToken cancellationToken)
        {
            var sql = @"SELECT SystemMenu.Id,SystemMenu.`Name`,SystemMenu.`Code`,SystemMenu.Address,SystemMenu.Icon,SystemMenu.MenuType,SystemMenu.ParentId 
                        FROM SystemUser
                        join SystemRole on SystemUser.RoleId=SystemRole.Id
                        join SystemRoleMenu on SystemRole.Id=SystemRoleMenu.RoleId
                        join SystemMenu on SystemRoleMenu.MenuId=SystemMenu.Id
                        where SystemUser.Id=@UserId and SystemMenu.MenuType !=3";

            var menus = await _dapper.QueryAsync<MenuDto>(sql, new { UserId = request.UserId });

            List<MenuTreeDto> dtos = new List<MenuTreeDto>();
            if (menus.Any())
            {
                var parentMenus = menus.Where(c => String.IsNullOrEmpty(c.ParentId));
                foreach (var parent in parentMenus)
                {
                    var dto = new MenuTreeDto
                    {
                        Id = parent.Id,
                        Name = parent.Name,
                        Code = parent.Code,
                        Address = parent.Address,
                        Icon = parent.Icon,
                        MenuType = parent.MenuType,
                        ParentId = parent.ParentId,
                        Children = new List<MenuTreeDto>()
                    };
                    dto.Children = GetChildren(parent, menus);
                    dtos.Add(dto);
                }
            }

            return new ResultDto<List<MenuTreeDto>>
            {
                State = 1,
                Data = dtos
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="menuEntities"></param>
        /// <returns></returns>
        private List<MenuTreeDto> GetChildren(MenuDto parent, IEnumerable<MenuDto> menuEntities)
        {
            List<MenuTreeDto> childrenDto = new List<MenuTreeDto>();
            var children = menuEntities.Where(t => t.ParentId == parent.Id).ToList();
            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    MenuTreeDto dto = new MenuTreeDto
                    {
                        Id = child.Id,
                        Name = child.Name,
                        Code = child.Code,
                        Address = child.Address,
                        Icon = child.Icon,
                        MenuType = child.MenuType,
                        ParentId = parent.Id,
                        Children = new List<MenuTreeDto>()
                    };

                    dto.Children = GetChildren(child, menuEntities);
                    childrenDto.Add(dto);
                }
            }

            return childrenDto;
        }
    }

}
