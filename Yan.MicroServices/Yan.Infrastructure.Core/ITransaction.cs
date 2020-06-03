using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Infrastructure.Core
{
    /// <summary>
    /// 事物管理接口
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// 获取当前事物
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction GetCurrentTransaction();

        /// <summary>
        /// 当前事物是否开启
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// 开启事物
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// 提交事物
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CommitTransactionAsync(IDbContextTransaction transaction);

        /// <summary>
        /// 回滚事物
        /// </summary>
        void RollbackTransaction();
    }
}
