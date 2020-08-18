using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yan.Dapper;
using Yan.Idp.Models;

namespace Yan.Idp.Quickstart
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public ResourceOwnerPasswordValidator(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var sql = @"Select * from SystemUser where UserName=@name and Password=@password ";

            SystemUser user = await _dapper.QueryFirstOrDefaultAsync<SystemUser>(sql, new { name = context.UserName, password = context.Password });
            
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid client credential");
            }
            else
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                        new Claim("name",user.UserName),
                        new Claim("id",user.Id.ToString()),
                        new Claim("realname",user.RealName),
                        new Claim("email",user.Email),
                        new Claim("roleid",user.RoleId)
                    }
                );
            }

           // return Task.CompletedTask;
        }
    }
}
