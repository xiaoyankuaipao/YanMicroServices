using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.SystemService.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemRoleMenu : Entity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuId { get; set; }
    }
}
