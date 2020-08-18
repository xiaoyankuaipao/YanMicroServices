using MediatR;
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
    public class DeleteArticleCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string ArticleId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleRepository"></param>
        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            await _articleRepository.DeleteAsync(request.ArticleId);
            await _articleRepository.UnitOfWork.SaveEntitiesAsync();

            return new HandleResultDto { State = 1 };
        }
    }

}
