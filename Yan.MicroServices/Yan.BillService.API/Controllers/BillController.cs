using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yan.BillService.API.Application.Commands;
using Yan.BillService.API.Application.Queries;
using Yan.BillService.API.Models;

namespace Yan.BillService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/billmanage/[controller]/[action]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public BillController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// 创建账单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateBill()
        {
            return await _mediator.Send(new CreateBillCommand { Person = "ycp" }, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        [HttpGet("{billId}")]
        public async Task<bool> DeleteBill(string billId)
        {
            return await _mediator.Send(new DeleteBillCommand { BillId = billId }, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 创建账单项
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateBillItem([FromBody] CreateBillItemCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 删除账单项
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteBillItem([FromBody] DeleteBillItemCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 更新账单项
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateBillItem([FromBody] UpdateBillItemCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 统计当年当月花费
        /// </summary>
        [HttpGet]
        public async Task<CostStatisticsOutput> GetCostStatics()
        {
            return await _mediator.Send(new CostStatisticsQuery(), HttpContext.RequestAborted);
        }
    }
}
