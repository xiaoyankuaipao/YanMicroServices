using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Events;
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
            this.Category = categoryName;

            //当构造一个全新的 ArticleCategory 的时候，这里我们可以定义一个领域事件：ArticleCategoryCreate
            //这个领域事件的构造函数的入参就是一个 ArticleCategory，
            //当我们调用 ArticleCategory 的构造函数时，我们的行为就是创建一个全新的 ArticleCategory
            //所以在这里添加一个领域事件
            //领域事件的构造和添加都应该是在领域模型的方法内完成的
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
