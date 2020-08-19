using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Events;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory : Entity<string>,IAggregateRoot
    {
        /// <summary>
        /// 文章分类名称
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ArticleCategory()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        public ArticleCategory(string categoryName)
        {
            this.Id= SnowflakeId.Default().NextId().ToString();
            this.Category = categoryName;
            this.AddDomainEvent(new ArticleCategoryCreateDomainEvent(this));
        }

        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="newName"></param>
        public void ChangeName(string newName)
        {
            this.Category = newName;
        }


    }
}
