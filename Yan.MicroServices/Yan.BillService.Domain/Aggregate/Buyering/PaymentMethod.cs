using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.BillService.Domain.Aggregate.Buyering
{
    public class PaymentMethod:Entity<int>
    {
        private string _alias;
        private string _cardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;

        private int _cardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod() { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            _cardNumber = cardNumber;
            _securityNumber = securityNumber;
            _cardHolderName = cardHolderName;

            _alias = alias;
            _expiration = expiration;
            _cardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return _cardTypeId == cardTypeId
                && _cardNumber == cardNumber
                && _expiration == expiration;
        }
    }
}

