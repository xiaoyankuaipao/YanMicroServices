using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.Views.Shared.Components.SubMenuList
{
    [ViewComponent(Name = "SubMenuList")]
    public class SubMenuModel : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ElMenu subMenu,List<ElMenu> menus)
        {
            return View("SubMenu", new ElSubMenu{SubMenu = subMenu,Menus = menus});
        }
    }
}
