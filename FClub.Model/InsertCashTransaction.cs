﻿using FClub.Core;
using System;

namespace FClub.Model
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(IIdentifier identifier, User user, decimal amount)
            : base(identifier, user, amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot insert negative amount", nameof(amount));
            }
        }
    }
}