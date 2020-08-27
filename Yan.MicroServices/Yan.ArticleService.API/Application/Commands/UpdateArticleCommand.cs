using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Core.Dtos;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateArticleCommand : IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public ArticleDto ArticleDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> TagIds { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRepository"></param>
        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleWithTagsById(request.ArticleDto.Id, cancellationToken);

            article.UpdateArticle(request.ArticleDto.CategoryId, request.ArticleDto.Title, request.ArticleDto.Remark,
                request.ArticleDto.Content, request.ArticleDto.Value);
            article.SetTags(request.TagIds);

            await _articleRepository.UpdateAsync(article);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync();

            return new HandleResultDto { State = 1 };
        }
    }
}
