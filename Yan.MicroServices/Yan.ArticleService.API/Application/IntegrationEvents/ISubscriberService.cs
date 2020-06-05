using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Application.IntegrationEvents
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubscriberService
    {
        /// <summary>
        /// 
        /// </summary>
        void ArticleCategoryCreated(ArticleCategoryCreatedIntegrationEvent @event);
    }
}
