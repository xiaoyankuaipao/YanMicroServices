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
    public class MenuComboxTreeQuery:IRequest<ResultDto<List<ComboxTreeDto>>>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class MenuComboxTreeQueryHandler : IRequestHandler<MenuComboxTreeQuery, ResultDto<List<ComboxTreeDto>>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public MenuComboxTreeQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<ComboxTreeDto>>> Handle(MenuComboxTreeQuery request, CancellationToken cancellationToken)
        {
            var sql = @"select Id,Name,Code,Address,Icon,MenuType,ParentId from SystemMenu;";

            var menus = await _dapper.QueryAsync<MenuDto>(sql);

            List<ComboxTreeDto> dtos = new List<ComboxTreeDto>();
            if (menus.Any())
            {
                var parentMenus = menus.Where(c => String.IsNullOrEmpty(c.ParentId));
                foreach (var parent in parentMenus)
                {
                    var dto = new ComboxTreeDto
                    {
                        Id = parent.Id,
                        Text = parent.Name,
                        Children = new List<ComboxTreeDto>()
                    };
                    dto.Children = GetChildren(parent, menus);
                    dtos.Add(dto);
                }
            }

            return new ResultDto<List<ComboxTreeDto>>
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
        private List<ComboxTreeDto> GetChildren(MenuDto parent, IEnumerable<MenuDto> menuEntities)
        {
            List<ComboxTreeDto> childrenDto = new List<ComboxTreeDto>();
            var children = menuEntities.Where(t => t.ParentId == parent.Id).ToList();
            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    ComboxTreeDto dto = new ComboxTreeDto
                    {
                        Id = child.Id,
                        Text = child.Name,
                        Children = new List<ComboxTreeDto>()
                    };

                    dto.Children = GetChildren(child, menuEntities);
                    childrenDto.Add(dto);
                }
            }

            return childrenDto;
        }

    }
}
