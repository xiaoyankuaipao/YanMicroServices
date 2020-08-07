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
    public class UpdateArticleCategoryCommand : IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateArticleCategoryCommandHandler : IRequestHandler<UpdateArticleCategoryCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleCategoryRepository"></param>
        public UpdateArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository)
        {
            this._articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(UpdateArticleCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity =await _articleCategoryRepository.GetAsync(request.Id, cancellationToken);
            entity.ChangeName(request.CategoryName);
            await _articleCategoryRepository.UpdateAsync(entity);

            var result =await _articleCategoryRepository.UnitOfWork.SaveEntitiesAsync();
            return new HandleResultDto
            {
                State = result == true ? 1 : 0
            };
        }
    }

}
