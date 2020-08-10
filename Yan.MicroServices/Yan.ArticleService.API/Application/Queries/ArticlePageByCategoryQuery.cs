using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public class ArticlePageByCategoryQuery:IRequest<PageResultDto<ArticleListDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rows { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueryArticlePageByCategoryQueryHandler : IRequestHandler<ArticlePageByCategoryQuery, PageResultDto<ArticleListDto>>
    {
        /// <summary>
        ///  
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public QueryArticlePageByCategoryQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResultDto<ArticleListDto>> Handle(ArticlePageByCategoryQuery request, CancellationToken cancellationToken)
        {
            StringBuilder sqlBuilder = new StringBuilder(@"select SQL_CALC_FOUND_ROWS Articles.Id,Articles.Title,Articles.Remark,Articles.ReadCount,Articles.likeCount,Articles.CreateTime,ArticleCategory.Category as CategoryName
                                                         from Articles
                                                         LEFT JOIN ArticleCategory on Articles.CategoryId=  ArticleCategory.Id ");
            if (request.CategoryId != 0)
            {
                sqlBuilder.Append("where Articles.CategoryId=@CategoryId ");
            }
            sqlBuilder.Append("limit @Skip,@Take;");
            sqlBuilder.Append("SELECT FOUND_ROWS() as Total;");

            var sql = sqlBuilder.ToString();
            var dapperPageInfo = await _dapper.QueryPage<ArticleListDto>(sql, new { CategoryId = request.CategoryId, Skip = (request.Page - 1) * request.Rows, Take = request.Rows });


            PageResultDto<ArticleListDto> result = new PageResultDto<ArticleListDto>()
            {
                State = 1,
                Result = new ResultPage<ArticleListDto>()
                {
                    TotalCount = (int)dapperPageInfo.TotalCount,
                    Data = dapperPageInfo.Data
                }
            };

            return result;
        }
    }
}
