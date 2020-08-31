using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Infrastructure.Repositories;

namespace Yan.BillService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBillCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Person { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBillRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public CreateBillCommandHandler(IBillRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = new Bill(request.Person);
            await _repository.AddAsync(bill,cancellationToken);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }

}
