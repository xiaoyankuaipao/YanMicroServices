using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticleCategoryRepository : IRepository<ArticleCategory, string>
    {
        //自定义方法，特殊的逻辑
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleCategoryRepository : Repository<ArticleCategory, string, ArticleContext>, IArticleCategoryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ArticleCategoryRepository(ArticleContext context) : base(context)
        {
        }

        //自定义方法，特殊的逻辑
    }
}
