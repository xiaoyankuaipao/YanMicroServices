using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;

namespace Yan.SystemService.Infrastructure
{
    /// <summary>
    /// 要实现对SystemContext的事务处理
    /// 只需要创建一个类 并继承自TransactionBehavior即可。
    /// </summary>
    public class SystemContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<SystemContext, TRequest, TResponse>
    {
        public SystemContextTransactionBehavior(SystemContext dbContext, ICapPublisher capBus, ILogger<SystemContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
