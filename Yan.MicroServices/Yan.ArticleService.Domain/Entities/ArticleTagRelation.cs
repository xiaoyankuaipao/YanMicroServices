using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleTagRelation:Entity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ArticleId { get; set; }
    }
}
