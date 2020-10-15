using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Ordering;
using Yan.Infrastructure.Core;

namespace Yan.BillService.Infrastructure.Repositories
{
    public interface IOrderRepository: IRepository<Order, int>
    {
    }

    public class OrderRepository : Repository<Order, int, BillContext>, IOrderRepository
    {
        public OrderRepository(BillContext context) : base(context)
        {
        }

        
    }
}
