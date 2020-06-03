using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.Domain.Abstractions;

namespace Yan.Infrastructure.Core
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
        where TDbContext : EFContext
    {
        /// <summary>
        /// 
        /// </summary>
        protected virtual TDbContext DbContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(TDbContext context)
        {
            this.DbContext = context;
        }

        /// <summary>
        /// EFContext 实现了IUnitOfWork接口
        /// </summary>
        public virtual IUnitOfWork UnitOfWork => DbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Add(entity).Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Add(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entity)
        {
            return DbContext.Update(entity).Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Remove(TEntity entity)
        {
            DbContext.Remove(entity);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveAsync(TEntity entity)
        {
            return Task.FromResult(Remove(entity));
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>, IAggregateRoot
        where TDbContext : EFContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(TDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 删除
        /// 实际上还是根据Id从DbContext里面获取Entity 然后再去Remove
        /// 这样的好处是可以跟踪对象的状态，坏处是需要操作两遍数据库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(TKey id)
        {
            var entity = DbContext.Find<TEntity>(id);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }

        // <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey id)
        {
            return DbContext.Find<TEntity>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbContext.FindAsync<TEntity>(id, cancellationToken);
        }

    }
}
