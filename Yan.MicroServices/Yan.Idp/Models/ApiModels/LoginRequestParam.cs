using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.Idp.Models.ApiModels
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientId { get; set; }
    }
}
