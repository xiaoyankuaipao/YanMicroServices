using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Domain.Aggregate.Ordering;
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
        //private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public CreateBillCommandHandler(IBillRepository repository/*,IOrderRepository orderRepository*/)
        {
            _repository = repository;
            //_orderRepository = orderRepository;
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
            await _repository.AddAsync(bill, cancellationToken);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //var address = new Address("1", "2", "3", "4", "5");
            //var order = new Order("1", "ycp", address, 1, "2", "2", "2", DateTime.Now);


            //order.AddOrderItem(1, "abc", 10, 2, "http://www.baidu.com");


            //await _orderRepository.AddAsync(order);
            //await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }

}
