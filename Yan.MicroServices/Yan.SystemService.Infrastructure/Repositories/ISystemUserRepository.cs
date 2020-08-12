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
    public interface ISystemUserRepository:IRepository<SystemUser,string>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class SystemUserRepository : Repository<SystemUser, string, SystemContext>, ISystemUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SystemUserRepository(SystemContext context) : base(context)
        {
        }
    }
}
