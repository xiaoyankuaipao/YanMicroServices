using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yan.AdminUI2.ViewComponents
{
    /// <summary>
    /// page table view component
    /// </summary>
    [ViewComponent(Name = "PageTable")]
    public class PageTableViewComponent:ViewComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //return View("Default");
            return View();
        }
    }



}
