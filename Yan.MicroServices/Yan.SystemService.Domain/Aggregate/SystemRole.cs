using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yan.Domain.Abstractions;
using Yan.SystemService.Domain.Entities;
using Yan.Utility;

namespace Yan.SystemService.Domain.Aggregate
{
    public class SystemRole : Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SystemRoleMenu> SystemRoleMenus { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        public SystemRole(string name, string displayName)
        {
            this.Id = SnowflakeId.Default().NextId().ToString();
            this.Name = name;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        public void UpdateRole(string name, string displayName)
        {
            this.Name = name;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleMenus"></param>
        public void UpdateRoleMenu(string[] menuIds)
        {
            if (this.SystemRoleMenus == null)
            {
                this.SystemRoleMenus = new List<SystemRoleMenu>();
            }
            foreach (var menuId in menuIds)
            {
                var temp = this.SystemRoleMenus.FirstOrDefault(c => c.MenuId == menuId);
                if (temp == null)
                {
                    this.SystemRoleMenus.Add(new SystemRoleMenu { RoleId = this.Id, MenuId = menuId });
                }
            }
            for (int i = 0; i < this.SystemRoleMenus.Count; i++)
            {
                var temp = this.SystemRoleMenus[i];
                if (!menuIds.Contains(temp.MenuId))
                {
                    this.SystemRoleMenus.Remove(temp);
                    i--;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>

        public void DeleteRoleMenu()
        {
            if (this.SystemRoleMenus != null)
            {
                this.SystemRoleMenus.Clear();
            }
        }

    }
}
