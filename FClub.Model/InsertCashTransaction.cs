using System;

namespace FClub.Model
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount)
            : base(user, amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot insert negative amount", nameof(amount));
            }
        }
    }
}
