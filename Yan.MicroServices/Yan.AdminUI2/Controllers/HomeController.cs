using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Yan.AdminUI2.Models;

namespace Yan.AdminUI2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<ElMenu> menus = new List<ElMenu>()
            {
                new ElMenu("导航一","path","name1", "el-icon-menu","1","0" ),
                new ElMenu("分组一","path","name2","el-icon-location","2","1"),
                new ElMenu("选项一","/home/index2","index2","el-icon-location","3","2"),
                new ElMenu("选项二","/home/index3","index3","el-icon-location","4","2"),
                new ElMenu("分组二","/path","name5","el-icon-location","5","1"),
                new ElMenu("分组三","path","name6","el-icon-location","6","1"),
                new ElMenu("选项一","/home/index2","name7","el-icon-location","7","6"),
                new ElMenu("选项二","/home/index2","name8","el-icon-location","8","6"),

                new ElMenu("导航二","path","name9", "el-icon-menu","9","0"),
                new ElMenu("分组一","path","name10","el-icon-location","10","9"),
                new ElMenu("选项一","/home/index2","name11","el-icon-location","11","10"),
                new ElMenu("选项二","/home/index2","name12","el-icon-location","12","10"),
                new ElMenu("分组二","/path","name13","el-icon-location","13","9"),

                new ElMenu("导航三","/path","name", "el-icon-menu","14","0")
            };
            return View(menus);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Index2()
        {
            var videos = new List<VideoViewModel>()
            {
                new VideoViewModel()
                {
                    Id = "10.100.32.131", Label = "10.100.32.131",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_1",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.132", Label = "10.100.32.132",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch5_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch5_1",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.133", Label = "10.100.32.133",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch6_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch6_1",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.134", Label = "10.100.32.134",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch7_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch7_1",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.135", Label = "10.100.32.135",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch14_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch14_1",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.136", Label = "10.100.32.136",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.137", Label = "10.100.32.137",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.138", Label = "10.100.32.138",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    Children = new List<VideoViewModel>()
                },
                new VideoViewModel()
                {
                    Id = "10.100.32.139", Label = "10.100.32.139",
                    MainCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    SubCodeStreamUrl = "http://10.100.32.140:8002/live?port=1935&app=videoapp&stream=streamch3_0",
                    Children = new List<VideoViewModel>()
                }

            };

            var js = JsonConvert.SerializeObject(videos);

            ViewBag.Data = videos;
            return View();
        }

        [Authorize]
        public IActionResult Index3()
        {
            return View();
        }

        [Authorize]
        public IActionResult DashBoard()
        {
            return View();
        }

        [Authorize]
        public IActionResult GetArticleLst(int page,int limit)
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


            return Json(result);
        }
    }
}
