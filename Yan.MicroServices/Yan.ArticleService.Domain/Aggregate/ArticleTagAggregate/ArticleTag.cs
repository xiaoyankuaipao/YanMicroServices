using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Entities;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate
{
    /// <summary>
    /// 文章标签
    /// </summary>
    public class ArticleTag: Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Tag { get; set; }

        public List<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
