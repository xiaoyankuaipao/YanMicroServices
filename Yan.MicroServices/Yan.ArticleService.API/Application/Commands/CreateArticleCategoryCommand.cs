using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [NoTransaction] //表示对于这个命令的处理，不开启数据库事务，例如在查询的时候 就不需要开启事务
    public class CreateArticleCategoryCommand:IRequest<long>
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage ="分类名称不能为空")]
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, long>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleCategoryRepository"></param>
        public CreateArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository)
        {
            this._articleCategoryRepository = articleCategoryRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  async Task<long> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
        {
            var articleCategory = new ArticleCategory(request.CategoryName);

            _articleCategoryRepository.Add(articleCategory);
            await _articleCategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return articleCategory.Id;
        }
    }
}
