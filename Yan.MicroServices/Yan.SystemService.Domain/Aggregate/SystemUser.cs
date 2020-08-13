using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.SystemService.Domain.Aggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemUser: Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="realName"></param>
        /// <param name="email"></param>
        public SystemUser(string userName, string password, string realName, string email)
        {
            this.Id = SnowflakeId.Default().NextId().ToString();
            this.UserName = userName;
            this.Password = password;
            this.RealName = realName;
            this.Email = email;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="realName"></param>
        /// <param name="email"></param>
        public void UpdateUser(string userName, string password, string realName, string email)
        {
            this.UserName = userName;
            this.Password = password;
            this.RealName = realName;
            this.Email = email;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        public void SetUserRole(string roleId)
        {
            this.RoleId = roleId;
        }

    }
}
