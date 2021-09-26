using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public IActionResult Create(Test t)
        {
            return View();
        }
    }

    public class Test
    {

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
