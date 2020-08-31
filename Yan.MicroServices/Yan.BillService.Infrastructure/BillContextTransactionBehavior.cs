using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;

namespace Yan.BillService.Infrastructure
{
    /// <summary>
    /// 要实现对BillContext的事务处理
    /// 只需要创建一个类 并继承自TransactionBehavior即可。
    /// </summary>
    public class BillContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<BillContext, TRequest, TResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="capBus"></param>
        /// <param name="logger"></param>
        public BillContextTransactionBehavior(BillContext dbContext, ICapPublisher capBus, ILogger<BillContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
