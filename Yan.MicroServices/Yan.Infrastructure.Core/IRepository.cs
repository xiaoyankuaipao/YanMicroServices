using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.Domain.Abstractions;

namespace Yan.Infrastructure.Core
{
    /// <summary>
    /// 仓储接口
    /// TEntity的约束是继承Entity,IAggregateRoot,
    /// 也就是说仓储里面存储的对象必须是一个聚合根对象
    /// </summary>
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// 工作单元
        /// 仓储要有获取 UnitOfWork 的能力
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(TEntity entity);
    }

    /// <summary>
    /// 泛型主键的 仓储 接口
    /// 定义Id相关操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
