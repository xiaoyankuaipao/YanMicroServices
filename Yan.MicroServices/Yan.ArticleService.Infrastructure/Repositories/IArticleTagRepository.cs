using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticleTagRepository:IRepository<ArticleTag,int>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleTagRepository : Repository<ArticleTag, int, ArticleContext>, IArticleTagRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ArticleTagRepository(ArticleContext context) : base(context)
        {
        }
    }

}
