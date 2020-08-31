using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.API.Models;
using Yan.Dapper;

namespace Yan.BillService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class CostStatisticsQuery:IRequest<CostStatisticsOutput>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class CostStatisticsQueryHandler : IRequestHandler<CostStatisticsQuery, CostStatisticsOutput>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public CostStatisticsQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public async Task<CostStatisticsOutput> Handle(CostStatisticsQuery request, CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            var startYear = new DateTime(year, 1, 1, 0, 0, 0);
            var endYear = new DateTime(year, 12, 31, 23, 59, 59);
            var yearSql = @"select SUM(TotalCost) as TheYearCost from Bill where BillCreateTime>@begin and BillCreateTime<@end;";
            var yearResult = await _dapper.GetResult<decimal>(yearSql, new { begin = startYear, end = endYear });

            DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            //获得本月月初时间
            var startMonth = dt.AddDays(1 - dt.Day);
            //获得本月月末时间
            DateTime s = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            DateTime ss = s.AddDays(1 - s.Day);
            var endMonth = ss.AddMonths(1).AddDays(-1);

            var monthSql= @"select SUM(TotalCost) as TheYearCost from Bill where BillCreateTime>@begin and BillCreateTime<@end;";
            var monthResult= await _dapper.GetResult<decimal>(monthSql, new { begin = startMonth, end = endMonth });

            var result = new CostStatisticsOutput
            {
                TheYearCost = Math.Round(yearResult, 2).ToString(),
                TheMonthCost = Math.Round(monthResult, 2).ToString()
            };
            
            return result;
        }
    }
}
