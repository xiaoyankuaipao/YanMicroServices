using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Events
{
    /// <summary>
    /// 创建文件分类的领域事件
    /// 领域事件的处理放在应用层
    /// </summary>
   public  class ArticleCategoryCreateDomainEvent:IDomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public ArticleCategory ArticleCategory { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleCategory"></param>
        public ArticleCategoryCreateDomainEvent(ArticleCategory articleCategory)
        {
            this.ArticleCategory = articleCategory;
        }

    }
}
