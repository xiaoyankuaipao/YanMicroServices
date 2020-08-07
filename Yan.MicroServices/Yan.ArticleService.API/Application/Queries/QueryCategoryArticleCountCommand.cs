using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.Core.Dtos;
using Yan.Dapper;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryCategoryArticleCountCommand:IRequest<ResultDto<List<CategoryArticleCount>>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueryCategoryArticleCountCommandHandler : IRequestHandler<QueryCategoryArticleCountCommand, ResultDto<List<CategoryArticleCount>>>
    {
        /// <summary>
        ///  
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public QueryCategoryArticleCountCommandHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<CategoryArticleCount>>> Handle(QueryCategoryArticleCountCommand request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT ArticleCategory.id as CategoryId,ArticleCategory.Category CategoryName,Count(Articles.Id) as ArticleCount  FROM ArticleCategory
                          LEFT JOIN Articles on ArticleCategory.Id = Articles.CategoryId
                          GROUP BY ArticleCategory.id";

            var entities = await _dapper.QueryAsync<CategoryArticleCount>(sql);

            ResultDto<List<CategoryArticleCount>> result = new ResultDto<List<CategoryArticleCount>>
            {
                State = 1,
                Data = entities?.ToList()
            };

            return result;
        }
    }
}
