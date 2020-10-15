﻿using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;

namespace Yan.BillService.Domain.Aggregate.Buyering
{
    /// <summary>
    /// 
    /// </summary>
    public class CardType:Enumeration
    {
        public static CardType Amex = new CardType(1, "Amex");
        public static CardType Visa = new CardType(2, "Visa");
        public static CardType MasterCard = new CardType(3, "MasterCard");

        public CardType(int id, string name) : base(id, name)
        {
        }
    }
}
