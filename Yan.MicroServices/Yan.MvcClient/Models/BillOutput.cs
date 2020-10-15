using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BillOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BillName { get; private set; }

        /// <summary>
        /// 账单记录人
        /// </summary>
        public string Person { get; private set; }

        /// <summary>
        /// 账单 总费用
        /// </summary>
        public decimal TotalCost { get; private set; }

        /// <summary>
        /// 账单创建时间
        /// </summary>
        public DateTime BillCreateTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="billName"></param>
        /// <param name="person"></param>
        /// <param name="totalCost"></param>
        /// <param name="billCreateTime"></param>
        public BillOutput(string id, string billName, string person, decimal totalCost, DateTime billCreateTime)
        {
            this.Id = id; this.BillName = billName; this.Person = person; this.TotalCost = totalCost; this.BillCreateTime = billCreateTime;
        }
    }
}
