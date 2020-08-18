using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.SystemService.API.Models
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }

        public string Email { get; set; }

        public string RoleId { get; set; }
    }
}
