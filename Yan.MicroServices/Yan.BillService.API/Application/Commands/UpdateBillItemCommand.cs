using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Entities;
using Yan.BillService.Infrastructure.Repositories;

namespace Yan.BillService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateBillItemCommand:IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BillItemTypeEnum BillItemTypeEnum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateBillItemCommandHandler : IRequestHandler<UpdateBillItemCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBillRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public UpdateBillItemCommandHandler(IBillRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UpdateBillItemCommand request, CancellationToken cancellationToken)
        {
            var bill = await _repository.GetBillAsync(request.BillId, cancellationToken);
            if (bill == null)
            {
                return false;
            }

            bill.UpDateBillItem(request.ItemId, request.Cost, request.BillItemTypeEnum, request.Remark);
            await _repository.UpdateAsync(bill);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }

}
