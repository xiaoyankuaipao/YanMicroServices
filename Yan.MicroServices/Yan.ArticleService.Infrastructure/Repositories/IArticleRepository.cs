using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Domain.Entities;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticleRepository:IRepository<Article,int>
    {
        //自定义方法，特殊的逻辑

        /// <summary>
        /// 
        /// </summary>
        /// <param name="article"></param>
        void AddArticle(Article article);
    }

    public class ArticleRepository : Repository<Article, int, ArticleContext>, IArticleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ArticleRepository(ArticleContext context) : base(context)
        {
        }

        //自定义方法，特殊的逻辑

        /// <summary>
        /// 
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(Article article)
        {
            var artilceEntity = this.Add(article);
            this.DbContext.SaveChanges();
            var set = this.DbContext.Set<ArticleTagRelation>();
            foreach (var tagid in article.TagIds)
            {
                set.Add(new ArticleTagRelation()
                {
                    ArticleId = artilceEntity.Id,
                    TagId = tagid
                });
            }
        }
    }

}
