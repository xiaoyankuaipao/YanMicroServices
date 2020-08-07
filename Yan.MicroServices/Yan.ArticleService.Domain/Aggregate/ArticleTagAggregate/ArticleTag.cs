using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleTag: Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ArticleCount { get; set; }
    }
}
