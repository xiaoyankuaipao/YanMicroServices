using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure
{
    /// <summary>
    /// 要实现对ArticleContext的事务处理
    /// 只需要创建一个类 并继承自TransactionBehavior即可。
    /// </summary>
    public class ArticleContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<ArticleContext, TRequest, TResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="capBus"></param>
        /// <param name="logger"></param>
        public ArticleContextTransactionBehavior(ArticleContext dbContext, ICapPublisher capBus, ILogger<ArticleContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
