using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// 保存数据库更改
        /// </summary>
        bool SaveChanges();
    }
}
