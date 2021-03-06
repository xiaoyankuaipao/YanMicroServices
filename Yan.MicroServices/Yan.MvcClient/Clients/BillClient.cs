﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yan.MvcClient.Models;

namespace Yan.MvcClient.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class BillClient
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public BillClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CostStatisticsOutput> GetCostStatics()
        {
            var result = await _client.GetStringAsync("/api/billmanage/Bill/GetCostStatics");

            var model = JsonConvert.DeserializeObject<CostStatisticsOutput>(result);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetRecentExpenditure()
        {
            var result = await _client.GetStringAsync("/api/billmanage/Bill/GetRecentExpenditure");

            var model = JsonConvert.DeserializeObject<List<string>>(result);

            return model;
        }

        /// <summary>
        /// 年度饼图
        /// </summary>
        /// <returns></returns>
        public async Task<List<EChartPieData>> GetYearPieData()
        {
            var result = await _client.GetStringAsync("/api/billmanage/Bill/GetYearPieData");

            var model = JsonConvert.DeserializeObject<List<EChartPieData>>(result);

            return model;
        }

        /// <summary>
        /// 月度饼图
        /// </summary>
        /// <returns></returns>
        public async Task<List<EChartPieData>> GetMonthPieData()
        {
            var result = await _client.GetStringAsync("/api/billmanage/Bill/GetMonthPieData");

            var model = JsonConvert.DeserializeObject<List<EChartPieData>>(result);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResultPage<BillOutput>> GetBillPage(BillPageInput input)
        {
            var response = new ResultPage<BillOutput>();

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/billmanage/Bill/GetBillPage", content);

            if (result.IsSuccessStatusCode)
            {
                var responseStr = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResultPage<BillOutput>>(responseStr);
            }

            return response;
            
        }

    }
}
