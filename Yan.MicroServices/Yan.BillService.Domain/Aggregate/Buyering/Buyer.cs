using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.BillService.Domain.Aggregate.Buyering
{
    public class Buyer : Entity<int>, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        public string Name { get; set; }

        private List<PaymentMethod> _paymentMethods;

        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string identity, string name) : this()
        {
            IdentityGuid = identity;
            Name = name;
        }

        public PaymentMethod VerifyOrAddPaymentMethod(
            int cardTypeId, string alias, string cardNumber,
            string securityNumber, string cardHolderName, DateTime expiration, int orderId)
        {
            var existingPayment = _paymentMethods
                .SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));

            if (existingPayment != null)
            {
                return existingPayment;
            }

            var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);

            _paymentMethods.Add(payment);

            return payment;
        }

    }
}
