using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.AdminUI2.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult User()
        {
            return View();
        }
    }
}
