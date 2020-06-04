using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleCategory : Entity<int>,IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
    }
}
