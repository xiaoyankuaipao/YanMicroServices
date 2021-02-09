using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.Admin.Clients;
using Yan.Admin.Clients.Account;
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

        public AccountController(AccountServiceClient accountServiceClient)
        {
            _accountServiceClient = accountServiceClient;
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

            return Json(result);
        }
    }
}
