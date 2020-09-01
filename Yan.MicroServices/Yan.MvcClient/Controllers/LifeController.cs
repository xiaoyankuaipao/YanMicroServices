using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.MvcClient.Clients;

namespace Yan.MvcClient.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LifeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly BillClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public LifeController(BillClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var model = await _client.GetCostStatics();
            return View(model);
        }


        /// <summary>
        /// 年度饼图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EChartPieData>> GetYearPieData()
        {
            return await _client.GetYearPieData();
        }

        /// <summary>
        /// 月度饼图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EChartPieData>> GetMonthPieData()
        {
            return await _client.GetMonthPieData();
        }
    }
}