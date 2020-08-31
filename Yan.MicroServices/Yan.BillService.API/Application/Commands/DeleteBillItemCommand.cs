using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Infrastructure.Repositories;
using Yan.Infrastructure.Core.Attributes;

namespace Yan.BillService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [UseTransaction]
    public class DeleteBillItemCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteBillItemCommandHandler : IRequestHandler<DeleteBillItemCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBillRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public DeleteBillItemCommandHandler(IBillRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(DeleteBillItemCommand request, CancellationToken cancellationToken)
        {
            var bill = await _repository.GetBillAsync(request.BillId,cancellationToken);
            if (bill == null)
            {
                return false;
            }
            else
            {
                bill.DeleteBillItem(request.ItemId);
            }

            await _repository.UpdateAsync(bill);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }

}
