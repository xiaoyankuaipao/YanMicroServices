using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Entities;
using Yan.Dapper;
using Yan.Utility;

namespace Yan.BillService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class RecentExpenditureQuery : IRequest<List<string>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class RecentExpenditureQueryHandler : IRequestHandler<RecentExpenditureQuery, List<string>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public RecentExpenditureQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<string>> Handle(RecentExpenditureQuery request, CancellationToken cancellationToken)
        {
            List<string> result = new List<string>();

            var sql = @"SELECT BillItem.Cost as Cost,BillItem.BillItemTypeEnum  as Type,Bill.Person as Person ,Bill.BillCreateTime as Time
                        FROM BillItem 
                        join Bill on BillItem.BillId = Bill.Id 
                        ORDER BY Bill.BillCreateTime DESC
                        LIMIT 0,10 ;";

            var sqlResult = await _dapper.QueryAsync<RecentTemp>(sql);

            if (sqlResult.Any())
            {
                foreach (var r in sqlResult)
                {
                    result.Add($"{r.Person} 在 {r.Time.ToString("yyyy-MM-dd HH:mm")} 记录：在 {((BillItemTypeEnum)r.Type).GetDescription()} 方面花费：{Math.Round(r.Cost, 2)} 元");
                }
            }


            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RecentTemp
    {
        public decimal Cost { get; set; }

        public int Type { get; set; }

        public string Person { get; set; }

        public DateTime Time { get; set; }
    }

}
