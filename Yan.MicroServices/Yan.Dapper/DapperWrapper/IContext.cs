using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsTransactionBegin { get; }

        /// <summary>
        /// 
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        void Rollback();
    }
}
