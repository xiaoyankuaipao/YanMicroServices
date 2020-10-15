using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.BillService.Domain.Aggregate.Ordering
{
    public class OrderItem:Entity<int>
    {
        //使用私有字段是与DDD聚合和域实体（而不是属性和属性集合）相一致的更好的封装
        private string _productName;
        private string _pictureUrl;
        private decimal _unitPrice;
        private decimal _discount;
        public int _units;

        public int ProductId { get; private set; }

        protected OrderItem()
        { }

        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            if (units <= 0)
            {
                throw new Exception("Invalid number of units");
            }

            if ((unitPrice * units) < discount)
            {
                throw new Exception("The total of order item is lower than applied discount");
            }

            ProductId = productId;

            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _units = units;
            _pictureUrl = pictureUrl;
        }

        public string GetPictureUri() => _pictureUrl;

        public decimal GetCurrentDiscount()
        {
            return _discount;
        }

        public int GetUnits()
        {
            return _units;
        }

        public decimal GetUnitPrice()
        {
            return _unitPrice;
        }

        public string GetOrderItemProductName() => _productName;

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new Exception("Discount is not valid");
            }

            _discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new Exception("Invalid units");
            }

            _units += units;
        }

    }
}
