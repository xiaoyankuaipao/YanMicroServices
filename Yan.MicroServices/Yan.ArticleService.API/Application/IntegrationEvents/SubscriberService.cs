using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Application.IntegrationEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        [CapSubscribe("ArticleCategoryCreated")]
        public void ArticleCategoryCreated(ArticleCategoryCreatedIntegrationEvent @event)
        {
            Console.WriteLine("我收到了 ArticleCategoryCreated 的集成事件");
        }
    }
}
