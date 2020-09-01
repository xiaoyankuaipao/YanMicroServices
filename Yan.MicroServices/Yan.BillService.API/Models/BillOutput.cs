using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.BillService.API.Models
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
    }
}
