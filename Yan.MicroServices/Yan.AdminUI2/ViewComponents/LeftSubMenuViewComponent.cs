using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.ViewComponents
{
    [ViewComponent(Name = "LeftSubMenu")]
    public class LeftSubMenuViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ElMenu subMenu, List<ElMenu> menus)
        {
            return View(new ElSubMenu { SubMenu = subMenu, Menus = menus });
        }
    }
}
