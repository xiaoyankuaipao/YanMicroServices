using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yan.BillService.Domain.Entities;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.BillService.Domain.Aggregate
{
    /// <summary>
    /// 账单
    /// </summary>
    public class Bill : Entity<string>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string BillName { get; private set; }

        /// <summary>
        /// 账单记录人
        /// </summary>
        public string Person { get; private set; }

        /// <summary>
        /// 账单项
        /// </summary>
        public List<BillItem> BillItems { get; private set; }

        /// <summary>
        /// 账单 总费用
        /// </summary>
        public decimal TotalCost { get; private set; }

        /// <summary>
        /// 账单创建时间
        /// </summary>
        public DateTime BillCreateTime { get; private set; }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="person"></param>
        public Bill(string person)
        {
            this.Id = SnowflakeId.Default().NextId().ToString();
            this.Person = person;
            this.TotalCost = 0;
            this.BillCreateTime = DateTime.Now;
            this.BillName = $"{this.BillCreateTime.ToString("yyyy-MM-dd")}-账单";
            this.BillItems = new List<BillItem>();
        }

        /// <summary>
        /// 为账单添加一个账单项 
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="itemType"></param>
        /// <param name="remark"></param>
        public void AddBillItem(BillItemTypeEnum itemType, decimal cost, string remark)
        {
            if (this.BillItems == null)
            {
                this.BillItems = new List<BillItem>();
            }

            BillItem item = new BillItem(itemType, cost, remark, this.Id);

            this.BillItems.Add(item);
            this.TotalCost += item.Cost;
        }

        /// <summary>
        /// 删除一个订单项
        /// </summary>
        /// <param name="itemId"></param>
        public void DeleteBillItem(string itemId)
        {
            if (this.BillItems == null || this.BillItems.Count == 0)
            {
                throw new Exception("该账单没有账单项可以删除");
            }

            var item = this.BillItems.FirstOrDefault(c => c.Id == itemId);
            if (item == null)
            {
                throw new Exception("要删除的账单项不存在");
            }

            this.BillItems.Remove(item);
            this.TotalCost -= item.Cost;
        }

        /// <summary>
        /// 修改一个订单项的信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="cost"></param>
        /// <param name="itemType"></param>
        /// <param name="remark"></param>
        public void UpDateBillItem(string itemId, decimal cost, BillItemTypeEnum itemType,string remark)
        {
            if (this.BillItems == null || this.BillItems.Count == 0)
            {
                throw new Exception("该账单没有账单项可以修改");
            }
            var item = this.BillItems.FirstOrDefault(c => c.Id == itemId);
            if (item == null)
            {
                throw new Exception("要修改的账单项不存在");
            }

            this.TotalCost -= item.Cost;
            item.UpdateBillItem(itemType, cost, remark);
            this.TotalCost += item.Cost;
        }

    }


}
