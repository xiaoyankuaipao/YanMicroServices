using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class Article : Entity<int>, IAggregateRoot
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

        /// <summary>
        /// 
        /// </summary>
        public List<int> TagIds { get; private set; }

        public Article()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="title"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="value"></param>
        public Article(int categoryId, string title, string remark, string content, string value, List<int> tagIds)
        {
            this.CategoryId = categoryId;
            this.Title = title;
            this.Remark = remark;
            this.Content = content;
            this.Value = value;
            this.ReadCount = 0;
            this.likeCount = 0;
            this.TagIds = tagIds;
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="title"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="value"></param>
        public void UpdateArticle(int categoryId, string title, string remark, string content, string value)
        {
            this.CategoryId = categoryId;
            this.Title = title;
            this.Remark = remark;
            this.Content = content;
            this.Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncrementLikeCout()
        {
            this.likeCount++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncrementReadCount()
        {
            this.ReadCount++;    
        }


    }
}
