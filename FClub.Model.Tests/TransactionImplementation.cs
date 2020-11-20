using System;
using FClub.Core;

namespace FClub.Model.Tests
{
    internal class TransactionImplementation : Transaction
    {
        public TransactionImplementation(IIdentifier identifier, User user, decimal amount)
            : base(identifier, user, amount)
        { }

        public TransactionImplementation(IIdentifier identifier, User user, decimal amount, DateTime date)
            : base(identifier, user, amount, date)
        { }
    }
}
