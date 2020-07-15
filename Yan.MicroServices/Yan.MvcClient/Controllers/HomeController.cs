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

namespace Yan.MvcClient.Controllers
{

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

       
        public IActionResult Index()
        {
            var name = User.Identity.Name;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Test()
        {
            return View();
        }

        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }


        public async Task< IActionResult>  Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

           return  RedirectToAction(nameof(Index));
        }

    }
}
