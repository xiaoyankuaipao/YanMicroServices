using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class DeleteArticleCategoryCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleCategoryRepository"></param>
        public DeleteArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
        {
            await _articleCategoryRepository.DeleteAsync(request.CategoryId);
            var result = await _articleCategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new HandleResultDto
            {
                State = result == true ? 1 : 0
            };
        }
    }
}
