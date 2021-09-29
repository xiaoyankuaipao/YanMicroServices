using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.AdminUI2.Models;

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
            var model = new ArticleViewModel
            {
                Id = "1", Title = "2", Remark = "3"
            };

            return View(model);
        }
    }



}
