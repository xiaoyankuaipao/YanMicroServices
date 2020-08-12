using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.SystemService.Domain.Aggregate
{
    /// <summary>
    /// 
    /// </summary>
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
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        public SystemRole(string name, string displayName)
        {
            this.Id = "";
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

    }
}
