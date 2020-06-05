using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticleRepository:IRepository<Article,int>
    {
        //自定义方法，特殊的逻辑
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
    }

}
