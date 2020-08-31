using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Entities;
using Yan.BillService.Infrastructure.Repositories;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.BillService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [UseTransaction]
    public class CreateBillItemCommand:IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillId { get; set; }

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
        public string remark { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateBillItemCommandHandler : IRequestHandler<CreateBillItemCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBillRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public CreateBillItemCommandHandler(IBillRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CreateBillItemCommand request, CancellationToken cancellationToken)
        {
            var bill = await _repository.GetBillAsync(request.BillId, cancellationToken);
            if (bill == null)
            {
                return false;
            }

            bill.AddBillItem(request.BillItemTypeEnum, request.Cost, request.remark);
            await _repository.UpdateAsync(bill, cancellationToken);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }

}
