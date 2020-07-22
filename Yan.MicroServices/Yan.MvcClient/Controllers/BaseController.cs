using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Yan.MvcClient.Clients;
using Yan.MvcClient.ViewModel;

namespace Yan.MvcClient.Controllers
{
    public class BaseController : Controller
    {
        private readonly ArticleServiceClient _articleClient;

        public BaseController(ArticleServiceClient articleClient)
        {
            _articleClient = articleClient;
        }

        public async Task GetCountData()
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

            ViewBag.ArticleCountByCategory = articleCountViewModel;
        }

    }
}
