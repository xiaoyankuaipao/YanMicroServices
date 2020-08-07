using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Core.Dtos;
using Yan.Dapper;

namespace Yan.ArticleService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryArticleCommand: IRequest<ResultDto<ArticleOutputDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public int ArticleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueryArticleCommandHandler : IRequestHandler<QueryArticleCommand, ResultDto<ArticleOutputDto>>
    {
        /// <summary>
        ///  
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        /// <param name="articleRepository"></param>
        public QueryArticleCommandHandler(DapperHelper dapper, IArticleRepository articleRepository)
        {
            _dapper = dapper;
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<ArticleOutputDto>> Handle(QueryArticleCommand request, CancellationToken cancellationToken)
        {
            var sql = @"select 
                                Articles.Id,Articles.CategoryId,Articles.Title,Articles.Remark,Articles.Content,Articles.Value,Articles.CreateTime,
                                Articles.likeCount,Articles.ReadCount,ArticleCategory.Category as CategoryName ,ArticleTag.Tag
                        FROM Articles
                        LEFT JOIN ArticleCategory on Articles.CategoryId = ArticleCategory.id 
                        LEFT join ArticleTagRelation on Articles.Id=ArticleTagRelation.ArticleId
                        LEFT JOIN ArticleTag on ArticleTagRelation.TagId=ArticleTag.Id
                        where Articles.Id=@Id";

            var entities = await _dapper.QueryAsync<ArticleOutputTempDto>(sql, new { Id = request.ArticleId });

            ResultDto<ArticleOutputDto> result = new ResultDto<ArticleOutputDto>() { State = 1 };

            if (entities.Count() > 0)
            {
                result.Data = new ArticleOutputDto()
                {
                    CategoryName = entities.First().CategoryName,
                    ArticleDto = new ArticleDto() {
                        Id = entities.First().Id,
                        CategoryId = entities.First().CategoryId,
                        Title = entities.First().Title,
                        Remark = entities.First().Remark,
                        Content = entities.First().Content,
                        Value = entities.First().Value,
                        CreateTime = entities.First().CreateTime,
                        LikeCount = entities.First().LikeCount,
                        ReadCount = entities.First().ReadCount
                    },
                    TagNames = new List<string>()
                };
                foreach (var entity in entities)
                {
                    result.Data.TagNames.Add(entity.Tag);
                }
            }

            var artilceEntity = await _articleRepository.GetAsync(request.ArticleId);
            artilceEntity.IncrementReadCount();
            await _articleRepository.UpdateAsync(artilceEntity);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync();

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleOutputTempDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string  CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }
    }
}
