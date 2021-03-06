﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.MvcClient.Clients;
using Yan.MvcClient.ViewModel;

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
            var near = await _client.GetRecentExpenditure();
            LifeIndexViewModel vm = new LifeIndexViewModel
            {
                StatisticsOutput = model,
                NearCosts = near
            };
            return View(vm);
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

        /// <summary>
        /// 账单列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Bills(int index=1,int size=10)
        {
            var model = await _client.GetBillPage(new Models.BillPageInput
            {
                BeginTime = DateTime.Now.AddDays(-30),
                EndTime = DateTime.Now,
                Index = index,
                Size = size
            });

            return View();
        }

    }
}