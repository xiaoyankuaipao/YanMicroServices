using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SystemRole> GetSystemRoleWithNavById(string roleId, CancellationToken cancellationToken = default);
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
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SystemRole> GetSystemRoleWithNavById(string roleId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<SystemRole>().Include(x => x.SystemRoleMenus).FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        }
    }

}
