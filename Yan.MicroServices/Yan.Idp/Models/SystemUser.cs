using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.Idp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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
    }
}
