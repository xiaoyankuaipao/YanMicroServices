using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class Article:Entity<int>,IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReadCount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int likeCount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; private set; }
    }
}
