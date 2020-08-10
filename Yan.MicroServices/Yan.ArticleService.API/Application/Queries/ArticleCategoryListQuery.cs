using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.Core.Dtos;
using Yan.Dapper;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleCategoryListQuery:IRequest<PageResultDto<ArticleCategoryDto>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleCategoryListQueryHandler : IRequestHandler<ArticleCategoryListQuery, PageResultDto<ArticleCategoryDto>>
    {
        /// <summary>
        ///  
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public ArticleCategoryListQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResultDto<ArticleCategoryDto>> Handle(ArticleCategoryListQuery request, CancellationToken cancellationToken)
        {
            var sql = "select * from ArticleCategory";
            var entities = await _dapper.QueryAsync<ArticleCategory>(sql);

            PageResultDto<ArticleCategoryDto> result = new PageResultDto<ArticleCategoryDto>()
            {
                State = 1,
                Result = new ResultPage<ArticleCategoryDto>()
                {
                    TotalCount = entities.Count(),
                    Data = Mapper.Map<List<ArticleCategoryDto>>(entities)
                }
            };

            return result;
        }
    }
}
