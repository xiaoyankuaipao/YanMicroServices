using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yan.Infrastructure.Core;
using Yan.SystemService.Domain.Aggregate;
using Yan.SystemService.Domain.Entities;

namespace Yan.SystemService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemRoleRepository : IRepository<SystemRole, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        void DeleteRoleMenuAsnyc(string roleId);
    }

    /// <summary>
    /// 
    /// </summary>
    public class SystemRoleRepository : Repository<SystemRole, string, SystemContext>, ISystemRoleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SystemRoleRepository(SystemContext context) : base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void DeleteRoleMenuAsnyc(string roleId)
        {
            var rela = DbContext.Set<SystemRoleMenu>().AsQueryable().Where(c => c.RoleId == roleId);
            foreach (var r in rela)
            {
                DbContext.Set<SystemRoleMenu>().Remove(r);
            }

        }
    }

}
