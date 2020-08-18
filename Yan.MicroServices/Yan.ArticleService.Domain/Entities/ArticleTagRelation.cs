using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Entities
{
    /// <summary>
    /// 文章与标签关系实体
    /// </summary>
    public class ArticleTagRelation:Entity<int>
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagId { get; set; }
    }
}
