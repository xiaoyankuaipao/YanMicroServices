using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yan.Core.Dtos;
using Yan.MvcClient.Clients;
using Yan.MvcClient.ViewModel;

namespace Yan.MvcClient.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ArticleServiceClient _articleClient;

        /// <summary>
        /// 
        /// </summary>
        public HomeController(ArticleServiceClient articleClient)
        {
            _articleClient = articleClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var user = User;
            var ArticleList = await _articleClient.GetArticlePageByCategory("", 1);
            var pageTotalCount = ArticleList.TotalCount % 10 == 0 ? ArticleList.TotalCount / 10 : ArticleList.TotalCount / 10 + 1;
            ArticleListPageViewModel viewModel = new ArticleListPageViewModel
            {
                PageIndex = 1,
                PageTotalCount = pageTotalCount,
                CategoryId = "",
                ResultPage = ArticleList
            };
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IActionResult> ArticleList(string categoryId, int pageIndex = 1)
        {
            var ArticleList = await _articleClient.GetArticlePageByCategory(categoryId, pageIndex);

            var pageTotalCount = ArticleList.TotalCount % 10 == 0 ? ArticleList.TotalCount / 10 : ArticleList.TotalCount / 10 + 1;

            ArticleListPageViewModel viewModel = new ArticleListPageViewModel
            {
                PageIndex = pageIndex,
                PageTotalCount = pageTotalCount,
                CategoryId = categoryId,
                ResultPage = ArticleList
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Article(string id)
        {
            var viewModel = await _articleClient.GetArticleById(id);
            return View(viewModel);
        }

        public  async Task<HandleResultDto> LikeArticle(string id)
        {
            var result= await _articleClient.LikeIt(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);

        }

    }
}
