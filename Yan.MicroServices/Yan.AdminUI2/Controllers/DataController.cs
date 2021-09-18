using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<Paged<ArticleViewModel>> GetArticleLst(int page, int limit)
        {
            var lst = new List<ArticleViewModel>
            {
                new ArticleViewModel(){Id="1",Title = "C#",Remark =page.ToString()+" "+ limit.ToString()},
                new ArticleViewModel(){Id="2",Title = "Java",Remark = page.ToString()+" "+limit.ToString()},
                new ArticleViewModel(){Id="3",Title = "C++",Remark =page.ToString()+" "+ limit.ToString()},
                new ArticleViewModel(){Id="4",Title = "C",Remark = page.ToString()+" "+limit.ToString()},
            };

            Paged<ArticleViewModel> result = new Paged<ArticleViewModel>()
            {
                TotalCount = 110,
                Datas = lst
            };


            return result;
        }
    }
}
