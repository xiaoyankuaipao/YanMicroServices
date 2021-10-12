using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.ViewComponents
{
    [ViewComponent(Name = "LeftMenu")]
    public class LeftMenuViewComponent:ViewComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(List<ElMenu> menus)
        {
            return View(menus);
        }
    }
}
