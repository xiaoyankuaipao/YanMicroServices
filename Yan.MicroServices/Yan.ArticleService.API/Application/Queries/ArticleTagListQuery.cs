using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate;
using Yan.Core.Dtos;
using Yan.Dapper;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleTagListQuery:IRequest<ResultDto<List<ArticleTagDto>>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueryArticleTagListQueryHandler : IRequestHandler<ArticleTagListQuery, ResultDto<List<ArticleTagDto>>>
    {
        /// <summary>
        ///  
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public QueryArticleTagListQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<ArticleTagDto>>> Handle(ArticleTagListQuery request, CancellationToken cancellationToken)
        {
            string sql = "select * from ArticleTag";
            var entities = await _dapper.QueryAsync<ArticleTag>(sql);

            var result = new ResultDto<List<ArticleTagDto>>()
            {
                State = 1,
                Data = Mapper.Map<List<ArticleTagDto>>(entities),
                Message = ""
            };

            return result;
        }
    }

}
