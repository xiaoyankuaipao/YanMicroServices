using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    public class RoleByIdQuery:IRequest<ResultDto<RoleDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoleByIdQueryHandle : IRequestHandler<RoleByIdQuery, ResultDto<RoleDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public RoleByIdQueryHandle(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<RoleDto>> Handle(RoleByIdQuery request, CancellationToken cancellationToken)
        {
            var sql = @" select Id,Name,DisplayName from SystemRole where Id=@Id";

            var role = await _dapper.QueryFirstOrDefaultAsync<RoleDto>(sql, new { Id = request.RoleId });
            if (role != null)
            {
                return new ResultDto<RoleDto>
                {
                    State = 1,
                    Data = role
                };
            }
            else
            {
                return new ResultDto<RoleDto>();
            }

        }
    }
}
