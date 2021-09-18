using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.AdminUI2.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
