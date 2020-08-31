using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.BillService.Domain.Aggregate;
using Yan.Infrastructure.Core;

namespace Yan.BillService.Infrastructure.Repositories
{
    public interface IBillRepository: IRepository<Bill, string>
    {
        //自定义方法，特殊的逻辑

        Task<Bill> GetBillAsync(string billId,CancellationToken cancellationToken = default);
    }

    public class BillRepository : Repository<Bill, string, BillContext>, IBillRepository
    {
        public BillRepository(BillContext context) : base(context)
        {
        }

        public async Task<Bill> GetBillAsync(string billId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Bill>().Include(x => x.BillItems).FirstOrDefaultAsync(c => c.Id == billId, cancellationToken);
        }
    }
}
