using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.Infrastructure.Core.Extensions;

namespace Yan.Infrastructure.Core
{
    /// <summary>
    /// EFContext
    /// </summary>
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        /// <summary>
        /// 
        /// </summary>
        protected IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        private ICapPublisher _capBus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options)
        {
            this._mediator = mediator;
            this._capBus = capBus;
        }

        #region IUnitOfWork 实现
        /// <summary>
        /// 数据持久化，并发布领域事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventAsync(this);//发送领域事件
            return true;
        }
        #endregion

        #region ITransaction 实现
        /// <summary>
        /// 当前事物
        /// </summary>
        private IDbContextTransaction _currentTransation;

        /// <summary>
        /// 获取当前事物
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransation;

        /// <summary>
        /// 事物是否开启
        /// </summary>
        public bool HasActiveTransaction => _currentTransation != null;

        /// <summary>
        /// 开启事物
        /// </summary>
        /// <returns></returns>
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransation != null)
            {
                return null;
            }

            _currentTransation = Database.BeginTransaction(_capBus, autoCommit: false);
            return Task.FromResult(_currentTransation);
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (_currentTransation != transaction)
            {
                throw new InvalidOperationException($"Transaction {transaction}");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            finally
            {
                if (_currentTransation != null)
                {
                    _currentTransation.Dispose();
                    _currentTransation = null;
                }
            }
        }

        /// 回滚事物
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransation?.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_currentTransation != null)
                {
                    _currentTransation.Dispose();
                    _currentTransation = null;
                }
            }
        }
        #endregion
    }
}
