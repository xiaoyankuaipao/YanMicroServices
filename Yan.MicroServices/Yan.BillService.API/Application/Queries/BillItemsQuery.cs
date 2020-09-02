using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.API.Models;
using Yan.BillService.Domain.Entities;
using Yan.Dapper;

namespace Yan.BillService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class BillItemsQuery:IRequest<List<BillItemOutput>>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BillItemsQueryHandler : IRequestHandler<BillItemsQuery, List<BillItemOutput>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public BillItemsQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<BillItemOutput>> Handle(BillItemsQuery request, CancellationToken cancellationToken)
        {
            var sqlBulder = new StringBuilder(@"select * from BillItem where BillId=@billId");

            var sqlResult = await _dapper.QueryAsync<BillItem>(sqlBulder.ToString(), new { billId = request.BillId });

            var result = Mapper.Map<List<BillItemOutput>>(sqlResult);

            return result;
            
        }
    }

}
