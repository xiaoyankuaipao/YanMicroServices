using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.SystemService.Domain.Aggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemMenu : Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int MenuType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="address"></param>
        /// <param name="icon"></param>
        /// <param name="menuType"></param>
        /// <param name="parentId"
        public SystemMenu(string name, string code, string address, string icon, int menuType,string parentId)
        {
            this.Id = SnowflakeId.Default().NextId().ToString();
            this.Name = name;
            this.Code = code;
            this.Address = address;
            this.Icon = icon;
            this.MenuType = menuType;
            this.ParentId = parentId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="address"></param>
        /// <param name="icon"></param>
        /// <param name="menuType"></param>
        /// <param name="parentId"></param>
        public void UpdateMenu(string name, string code, string address, string icon, int menuType, string parentId)
        {
            this.Name = name;
            this.Code = code;
            this.Address = address;
            this.Icon = icon;
            this.MenuType = menuType;
            this.ParentId = parentId;
        }
    }
}
