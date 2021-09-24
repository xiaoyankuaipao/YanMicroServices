using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.Views.Shared.Components.MenuList
{
    /// <summary>
    /// 
    /// </summary>
    [ViewComponent(Name = "MenuList")]
    public class MenuModel : ViewComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(List<ElMenu> menus )
        {
            return View("Menu", menus);
        }
    }
}
