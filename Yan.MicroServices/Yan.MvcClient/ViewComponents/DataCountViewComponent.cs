using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.MvcClient.Clients;
using Yan.MvcClient.ViewModel;

namespace Yan.MvcClient.ViewComponents
{
    /// <summary>
    /// 
    /// </summary>
    public class DataCountViewComponent:ViewComponent
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ArticleServiceClient _articleClient;

        /// <summary>
        /// 
        /// </summary>
        public DataCountViewComponent(ArticleServiceClient articleClient)
        {
            _articleClient = articleClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articleCountModel = await _articleClient.GetArticleStaticCountByCategory();
            List<ArticleCountViewModel> articleCountViewModel = new List<ArticleCountViewModel>();
            foreach (var item in articleCountModel)
            {
                articleCountViewModel.Add(new ArticleCountViewModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    ArticleCount = item.ArticleCount
                });
            }
            return View("DataCount", articleCountViewModel);
        }
    }
}
