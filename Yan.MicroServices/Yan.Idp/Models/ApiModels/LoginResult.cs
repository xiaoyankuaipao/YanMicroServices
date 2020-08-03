using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.Idp.Models.ApiModels
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Token Token { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }
    }
}
