using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Yan.Admin.Clients;
using Yan.Admin.Clients.Account;
using Yan.Admin.Clients.SystemManage;
using Yan.Admin.Models.UserLogin;

namespace Yan.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AccountServiceClient _accountServiceClient;

        private readonly SystemManageServiceClient _systemManageServiceClient;

        public AccountController(AccountServiceClient accountServiceClient, SystemManageServiceClient systemManageServiceClient)
        {
            _accountServiceClient = accountServiceClient;
            _systemManageServiceClient = systemManageServiceClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel viewModel)
        {
            var loginDto = new LoginDto
            {
                UserName = viewModel.UserName,
                Password = viewModel.Password,
                ClientId = "vue-manage"
            };
            var result = await _accountServiceClient.LoginInAsync(loginDto);

            if (result!=null)
            {
                var userInfo = await _systemManageServiceClient.GetUserInfo(result.Token.access_token);
                Sign(userInfo, result.Token.access_token);
            }

            return Json(result);
        }

        private void Sign(UserInfo userInfo,string token)
        {
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim("access_token", "Bearer " + token)
            }, "Basic"));

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,userPrincipal,new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(7),
                IsPersistent = true,
                AllowRefresh = true
            });
        }
    }
}
