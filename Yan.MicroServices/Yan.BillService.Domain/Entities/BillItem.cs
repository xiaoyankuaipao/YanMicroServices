using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.BillService.Domain.Entities
{
    /// <summary>
    /// 账单项
    /// </summary>
    public class BillItem : Entity<string>
    {
        /// <summary>
        /// 账单项类型
        /// </summary>
        public BillItemTypeEnum BillItemTypeEnum { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public BillItem()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="itemType"></param>
        /// <param name="remark"></param>
        /// <param name="billId"></param>
        public BillItem(BillItemTypeEnum itemType, decimal cost, string remark, string billId)
        {
            Id = SnowflakeId.Default().NextId().ToString();
            Cost = cost;
            BillItemTypeEnum = itemType;
            Remark = remark;
            BillId = billId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="cost"></param>
        /// <param name="remark"></param>
        public void UpdateBillItem(BillItemTypeEnum itemType, decimal cost, string remark)
        {
            Cost = cost;
            BillItemTypeEnum = itemType;
            Remark = remark;
        }
    }

    

}
