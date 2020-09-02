using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.API.Models;
using Yan.BillService.Domain.Aggregate;
using Yan.Core.Dtos;
using Yan.Dapper;

namespace Yan.BillService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class BillPageQuery:IRequest<ResultPage<BillOutput>>
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BillPageQueryHandler : IRequestHandler<BillPageQuery, ResultPage<BillOutput>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public BillPageQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultPage<BillOutput>> Handle(BillPageQuery request, CancellationToken cancellationToken)
        {
            StringBuilder sqlBuilder = new StringBuilder("select SQL_CALC_FOUND_ROWS ");
            sqlBuilder.Append(@" * FROM Bill where BillCreateTime>@beginTime and BillCreateTime<@endTime
                                ORDER BY BillCreateTime DESC ");
            sqlBuilder.Append("limit @Skip,@Take;");
            sqlBuilder.Append("SELECT FOUND_ROWS() as Total;");

            var sql = sqlBuilder.ToString();
            var dapperPageInfo = await _dapper.QueryPage<BillOutput>(sql, new { beginTime = request.BeginTime, endTime = request.EndTime, Skip = (request.Index - 1) * request.Size, Take = request.Size });

            ResultPage<BillOutput> result = new ResultPage<BillOutput>()
            {
                TotalCount = (int)dapperPageInfo.TotalCount,
                Data = dapperPageInfo.Data
            };

            return result;
        }


    }
}
