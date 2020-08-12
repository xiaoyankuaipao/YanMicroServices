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
    public interface ISystemMenuRepository:IRepository<SystemMenu,string>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class SystemMenuRepository : Repository<SystemMenu, string, SystemContext>, ISystemMenuRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SystemMenuRepository(SystemContext context) : base(context)
        {
        }
    }
}
