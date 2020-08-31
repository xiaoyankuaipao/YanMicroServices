using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate;
using Yan.Infrastructure.Core;

namespace Yan.BillService.Infrastructure.Repositories
{
    public interface IBillRepository: IRepository<Bill, string>
    {
        //自定义方法，特殊的逻辑
    }

    public class BillRepository : Repository<Bill, string, BillContext>, IBillRepository
    {
        public BillRepository(BillContext context) : base(context)
        {
        }
    }
}
