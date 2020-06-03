using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.Infrastructure.Core.Extensions;

namespace Yan.Infrastructure.Core
{
    /// <summary>
    /// 事物管理：基于Meditor 的 管道来实现
    /// 用来注入事物的管理过程
    /// </summary>
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TDbContext : EFContext
    {
        /// <summary>
        /// 
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        private TDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        private ICapPublisher _capBus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="capBus"></param>
        /// <param name="logger"></param>
        public TransactionBehavior(TDbContext dbContext, ICapPublisher capBus, ILogger logger)
        {
            _dbContext = dbContext;
            _capBus = capBus;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    {
                        using (_logger.BeginScope("TransactionContext:{transactionId}", transaction.TransactionId))
                        {
                            _logger.LogInformation("---开始事务{transactionId} ({@Command})", transaction.TransactionId, typeName, request);

                            response = await next();

                            _logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId, typeName);

                            await _dbContext.CommitTransactionAsync(transaction);

                            transactionId = transaction.TransactionId;
                        }
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);

                throw ex;
            }

        }
    }
}
