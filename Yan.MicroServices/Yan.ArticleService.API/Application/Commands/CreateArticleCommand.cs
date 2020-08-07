using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Core.Dtos;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [UseTransaction] //表示对于这个命令的处理，需要开启数据库事务
    public class CreateArticleCommand : IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ArticleDto ArticleDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> TagIds { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRepository"></param>
        public CreateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var articleCategory = new Article(request.ArticleDto.CategoryId, request.ArticleDto.Title, request.ArticleDto.Remark,
                request.ArticleDto.Content, request.ArticleDto.Value, request.TagIds);

            _articleRepository.AddArticle(articleCategory);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto()
            {
                State = 1
            };
        }
    }
}
