using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Yan.AdminUI2.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public EditResult Create(Test t)
        {
            return new EditResult()
            {
                IsSuccess = true
            };
        }
    }

    public class Test
    {

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }

    public class EditResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
