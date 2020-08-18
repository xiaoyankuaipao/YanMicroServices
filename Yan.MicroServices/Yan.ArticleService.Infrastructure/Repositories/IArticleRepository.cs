using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Domain.Entities;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticleRepository:IRepository<Article,string>
    {
        //自定义方法，特殊的逻辑

        Task<Article> GetArticleWithTagsById(string articleId, CancellationToken cancellationToken = default);
    }

    public class ArticleRepository : Repository<Article, string, ArticleContext>, IArticleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ArticleRepository(ArticleContext context) : base(context)
        {
        }

        public async Task<Article> GetArticleWithTagsById(string articleId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Article>().Include(x => x.ArticleTagRelations).FirstOrDefaultAsync(x => x.Id == articleId, cancellationToken);
        }


    }

}
