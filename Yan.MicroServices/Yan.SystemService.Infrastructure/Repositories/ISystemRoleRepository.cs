using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;
using Yan.SystemService.Domain.Aggregate;

namespace Yan.SystemService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemRoleRepository : IRepository<SystemRole, string>
    {

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
    }

}
