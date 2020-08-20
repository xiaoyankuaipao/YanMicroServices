using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// Dapper 工作单元
    /// </summary>
    public class DapperUnitOfWork<TContext> : IUnitOfWork where TContext : DapperDbContext
    {
        /// <summary>
        /// 上下文
        /// </summary>
        private readonly TContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 仓储集合
        /// </summary>
        private ConcurrentDictionary<Type, object> _repositories;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public DapperUnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.BeginTransaction();
        }

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new ConcurrentDictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// 保存数据库更改
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            try
            {
                _dbContext.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _dbContext.Rollback();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool dispoing)
        {
            if (!disposed)
            {
                if (dispoing)
                {
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

    }
}
