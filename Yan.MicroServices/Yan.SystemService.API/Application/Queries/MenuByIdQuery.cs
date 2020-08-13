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
    public class MenuByIdQuery:IRequest<ResultDto<MenuDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public string MenuId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MenuByIdQueryHandler : IRequestHandler<MenuByIdQuery, ResultDto<MenuDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public MenuByIdQueryHandler(DapperHelper dapper)
        {
            this._dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<MenuDto>> Handle(MenuByIdQuery request, CancellationToken cancellationToken)
        {
            var sql = @"select Id,Name,Code,Address,Icon,MenuType,ParentId from SystemMenu where Id=@Id";
            var menu = await _dapper.QueryFirstOrDefaultAsync<MenuDto>(sql, new { Id = request.MenuId });

            return new ResultDto<MenuDto>
            {
                State = 1,
                Data = menu
            };
        }
    }

}
