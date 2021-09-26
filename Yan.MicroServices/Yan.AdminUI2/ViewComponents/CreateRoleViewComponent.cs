using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yan.AdminUI2.ViewComponents
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRoleViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("CreateRole");
        }
    }

    public class EditRoleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("EditRole");
        }
    }
}
