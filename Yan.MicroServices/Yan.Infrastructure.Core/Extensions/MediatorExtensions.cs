using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yan.Domain.Abstractions;

namespace Yan.Infrastructure.Core.Extensions
{
    /// <summary>
    /// Mediator 扩展
    /// </summary>
    public static class MediatorExtensions
    {
        /// <summary>
        /// IMediator 扩展方法：发布领域事件
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventAsync(this IMediator mediator, DbContext dbContext)
        {
            var domainEntities = dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);//发布领域事件，并找到相应的Handler进行处理
            }
        }
    }
}
