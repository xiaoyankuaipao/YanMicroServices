using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate;
using Yan.ArticleService.Domain.Entities;
using Yan.Domain.Abstractions;

namespace Yan.ArticleService.Domain.Aggregate.ArticleAggregate
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public string CategoryId { get; private set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 文章内容html
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; private set; }

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int likeCount { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        ///  文章与标签的关系，这是一个实体，
        /// </summary>
        public List<ArticleTagRelation> ArticleTagRelations { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        public Article(string categoryId, string title, string remark, string content, string value)
        {
            this.CategoryId = categoryId;
            this.Title = title;
            this.Remark = remark;
            this.Content = content;
            this.Value = value;
            this.ReadCount = 0;
            this.likeCount = 0;
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 为文章打标签
        /// </summary>
        /// <param name="tagIds"></param>
        public void SetTags(List<string> tagIds)
        {
            if (this.ArticleTagRelations == null)
            {
                this.ArticleTagRelations = new List<ArticleTagRelation>();
            }
            foreach (var tagId in tagIds)
            {
                var temp = this.ArticleTagRelations.FirstOrDefault(c => c.TagId == tagId);
                if (temp == null)
                {
                    this.ArticleTagRelations.Add(new ArticleTagRelation { ArticleId = this.Id, TagId = tagId });
                }
            }
            for (var i = 0; i < this.ArticleTagRelations.Count; i++)
            {
                var temp = this.ArticleTagRelations[i];
                if (!tagIds.Contains(temp.TagId))
                {
                    this.ArticleTagRelations.Remove(temp);
                    i--;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="title"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="value"></param>
        public void UpdateArticle(string categoryId, string title, string remark, string content, string value)
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
