using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Application.IntegrationEvents;
using Yan.ArticleService.Domain.Events;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.API.Application.DomainEventHandlers
{
    /// <summary>
    /// "创建 ArticleCategory 领域事件"的处理程序,它是定义在应用层,
    /// 它继承自泛型的IDomainEventHandler，入参为OrderCreateDomainEvent
    /// 也就是我们需要处理的领域事件的类型
    public class ArticleCategoryCreateDomainEventHandler : IDomainEventHandler<ArticleCategoryCreateDomainEvent>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICapPublisher _capPublisher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capPublisher"></param>
        public ArticleCategoryCreateDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }


        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(ArticleCategoryCreateDomainEvent notification, CancellationToken cancellationToken)
        {
            //在这里，当我们创建一个新的文章分类 时，我们向EventBus发布一条集成事件(夸服务)
            _capPublisher.Publish("ArticleCategoryCreated", new ArticleCategoryCreatedIntegrationEvent(notification.ArticleCategory.Id));
            return Task.CompletedTask;
        }
    }
}
