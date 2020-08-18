using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.Dapper;

namespace Yan.SystemService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class UserBtnPermissionQuery : IRequest<ResultDto<string>>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
    }
   
    /// <summary>
    /// 
    /// </summary>
    public class UserBtnPermissionQueryHandler : IRequestHandler<UserBtnPermissionQuery, ResultDto<string>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public UserBtnPermissionQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<string>> Handle(UserBtnPermissionQuery request, CancellationToken cancellationToken)
        {
            var sql = @"SELECT SystemMenu.`Code` 
                        FROM SystemUser
                        join SystemRole on SystemUser.RoleId=SystemRole.Id
                        join SystemRoleMenu on SystemRole.Id=SystemRoleMenu.RoleId
                        join SystemMenu on SystemRoleMenu.MenuId=SystemMenu.Id
                        where SystemUser.Id=@UserId and SystemMenu.MenuType=3";

            var strLst = await _dapper.QueryAsync<string>(sql, new { UserId = request.UserId });

            ResultDto<string> result = new ResultDto<string>()
            {
                State = 1,
            };

            strLst.ToList().ForEach(t =>
            {
                result.Data += t + ",";
            });
            return result;
        }
    }
}
