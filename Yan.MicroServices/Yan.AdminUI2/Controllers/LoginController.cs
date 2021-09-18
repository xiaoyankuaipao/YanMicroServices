using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string returnUrl)
        {
            LoginViewModel vmModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(vmModel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel vModel)
        {
            ViewBag.VerifyCode = true;
            if (ModelState.IsValid)
            {
                if (vModel.UserName == "1" && vModel.Password == "1")
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, "admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "1"));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity), new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = null,
                            RedirectUri = "/home/index"
                        });

                    HttpContext.Session.SetString("token", "456");
                    if (!string.IsNullOrEmpty(vModel.ReturnUrl))
                    {
                        return Redirect(vModel.ReturnUrl);
                    }
                    else
                    {
                        return Redirect("~/");
                    }

                   
                }
                else
                {
                    ModelState.AddModelError("error", "用户名或者密码错误");
                }
            }
            else
            {
                ModelState.AddModelError("error", "输入信息的信息错误");
            }

            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
