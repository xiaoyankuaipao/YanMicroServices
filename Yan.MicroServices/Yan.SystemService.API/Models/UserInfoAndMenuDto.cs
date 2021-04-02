using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.SystemService.API.Models
{
    /// <summary>
    /// 用户信息和菜单
    /// </summary>
    public class UserInfoAndMenuDto
    {
        /// <summary>
        /// 
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<MenuTreeDto> MenuTreeDtos { get; set; }
    }
}
