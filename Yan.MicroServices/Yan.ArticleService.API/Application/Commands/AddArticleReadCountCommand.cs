using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.Infrastructure.Repositories;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class AddArticleReadCountCommand:IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public int ArticleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddArticleReadCountCommandHandler : IRequestHandler<AddArticleReadCountCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRepository"></param>
        public AddArticleReadCountCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddArticleReadCountCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetAsync(request.ArticleId);
            article.IncrementReadCount();

            await _articleRepository.UpdateAsync(article);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}
