using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Application.IntegrationEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleCategoryCreatedIntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string AricleCategoryId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aricleCategoryId"></param>
        public ArticleCategoryCreatedIntegrationEvent(string aricleCategoryId)
        {
            this.AricleCategoryId = aricleCategoryId;
        }
    }
}
