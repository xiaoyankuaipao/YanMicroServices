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
    public class DeleteBillCommand:IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBillRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public DeleteBillCommandHandler(IBillRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            var bill = await _repository.GetAsync(request.BillId,cancellationToken);
            if (bill == null)
            {
                return false;
            }

            await _repository.DeleteAsync(bill.Id,cancellationToken);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }

    }

}
