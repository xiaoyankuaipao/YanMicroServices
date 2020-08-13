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
    public class RoleListQuery:IRequest<PageResultDto<RoleDto>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, PageResultDto<RoleDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public RoleListQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResultDto<RoleDto>> Handle(RoleListQuery request, CancellationToken cancellationToken)
        {
            var sql = @" select Id,Name,DisplayName from SystemRole;";

            var roles = await _dapper.QueryAsync<RoleDto>(sql);

            return new PageResultDto<RoleDto>
            {
                State = 1,
                Result = new ResultPage<RoleDto>
                {
                    TotalCount = roles.Count(),
                    Data = roles.ToList()
                }
            };
        }
    }
}
