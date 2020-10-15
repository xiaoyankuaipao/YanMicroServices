using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.BillService.Domain.Aggregate.Ordering
{
    public class Order : Entity<int>, IAggregateRoot
    {
        private DateTime _orderDate;

        public Address Address { get; private set; }

        private int? _buyerId;
        public int? GetBuyerId => _buyerId;

        private string _description;

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        private int? _paymentMethodId;

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(string userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
                string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
        {
            _buyerId = buyerId;
            _paymentMethodId = paymentMethodId;
            _orderDate = DateTime.UtcNow;
            Address = address;
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }

                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _orderItems.Add(orderItem);
            }
        }
    }
}
