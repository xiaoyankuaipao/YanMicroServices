using MediatR;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Core.Dtos;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class LikeArticleCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public int ArticleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LikeArticleCommandHandler : IRequestHandler<LikeArticleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRepository"></param>
        public LikeArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetAsync(request.ArticleId);
            article.IncrementLikeCout();

            await _articleRepository.UpdateAsync(article);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync();

            return new HandleResultDto { State = 1 };
        }
    }
}
