using System;
using System.Collections.Generic;
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
        public void AddRoleMenu(SystemRoleMenu systemRoleMenu)
        {
            if (this.SystemRoleMenus == null)
            {
                this.SystemRoleMenus = new List<SystemRoleMenu> { systemRoleMenu };
            }
            else
            {
                this.SystemRoleMenus.Add(systemRoleMenu);
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
