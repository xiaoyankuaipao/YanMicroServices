using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.API.Models;
using Yan.BillService.Domain.Entities;
using Yan.Dapper;
using Yan.Utility;

namespace Yan.BillService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthPieDataQuery : IRequest<List<EChartPieData>>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class MonthPieDataQueryHandler : IRequestHandler<MonthPieDataQuery, List<EChartPieData>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public MonthPieDataQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<EChartPieData>> Handle(MonthPieDataQuery request, CancellationToken cancellationToken)
        {
            List<EChartPieData> result = new List<EChartPieData>();
            DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            //获得本月月初时间
            var startMonth = dt.AddDays(1 - dt.Day);
            //获得本月月末时间
            DateTime s = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            DateTime ss = s.AddDays(1 - s.Day);
            var endMonth = ss.AddMonths(1).AddDays(-1);

            var monthSql = @"SELECT BillItem.BillItemTypeEnum as Type,SUM(Cost) as Value FROM BillItem  
                            join Bill on BillItem.BillId = Bill.Id where Bill.BillCreateTime>=@beginTime and Bill.BillCreateTime<=@endTime
                            GROUP BY BillItem.BillItemTypeEnum;";
            var sqlResult = await _dapper.QueryAsync<PieDbData>(monthSql, new { beginTime = startMonth, endTime = endMonth });

            if (sqlResult.Any())
            {
                foreach (var r in sqlResult)
                {
                    result.Add(new EChartPieData
                    {
                        Value = Math.Round(r.Value, 2),
                        Name = ((BillItemTypeEnum)r.Type).GetDescription()
                    });
                }
            }

            return result;
        }
    }

}
