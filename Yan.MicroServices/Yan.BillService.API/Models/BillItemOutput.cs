using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.BillService.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BillItemOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账单项类型
        /// </summary>
        public string BillItemTypeEnum { get; set; }

        /// <summary>
        /// 账单项费用
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 账单项说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 账单Id
        /// </summary>
        public string BillId { get; set; }
    }
}
