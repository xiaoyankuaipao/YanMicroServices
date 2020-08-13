using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UserListQuery : IRequest<PageResultDto<UserDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rows { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserListQueryHandler : IRequestHandler<UserListQuery, PageResultDto<UserDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public UserListQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResultDto<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            StringBuilder sqlBuilder = new StringBuilder(@"select SQL_CALC_FOUND_ROWS Id,UserName,RealName,Email,RoleId from SystemUser ");

            sqlBuilder.Append("limit @Skip,@Take;");
            sqlBuilder.Append("SELECT FOUND_ROWS() as Total;");
            var sql = sqlBuilder.ToString();
            var dapperPageInfo = await _dapper.QueryPage<UserDto>(sql, new {  Skip = (request.Page - 1) * request.Rows, Take = request.Rows });

            PageResultDto<UserDto> result = new PageResultDto<UserDto>()
            {
                State = 1,
                Result = new ResultPage<UserDto>()
                {
                    TotalCount = (int)dapperPageInfo.TotalCount,
                    Data = dapperPageInfo.Data
                }
            };

            return result;

        }
    }
}
