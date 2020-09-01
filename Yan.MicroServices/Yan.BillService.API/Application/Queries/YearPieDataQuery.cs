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
    public class YearPieDataQuery:IRequest<List<EChartPieData>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class YearPieDataQueryHandler : IRequestHandler<YearPieDataQuery, List<EChartPieData>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public YearPieDataQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<EChartPieData>> Handle(YearPieDataQuery request, CancellationToken cancellationToken)
        {
            List<EChartPieData> result = new List<EChartPieData>();
            var year = DateTime.Now.Year;
            var startYear = new DateTime(year, 1, 1, 0, 0, 0);
            var endYear = new DateTime(year, 12, 31, 23, 59, 59);
            var yearSql = @"SELECT BillItem.BillItemTypeEnum as Type,SUM(Cost) as Value FROM BillItem  
                            join Bill on BillItem.BillId = Bill.Id where Bill.BillCreateTime>=@beginTime and Bill.BillCreateTime<=@endTime
                            GROUP BY BillItem.BillItemTypeEnum;";
            var sqlResult = await _dapper.QueryAsync<PieDbData>(yearSql, new { beginTime = startYear, endTime = endYear });

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
