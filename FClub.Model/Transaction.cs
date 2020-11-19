using System;

namespace FClub.Model
{
    public abstract class Transaction
    {
        private static int m_counter = 0;

        public Transaction(User user, decimal amount)
            : this(user, amount, DateTime.Now)
        { }

        public Transaction(User user, decimal amount, DateTime date)
        {
            Id = m_counter;
            User = user ?? throw new ArgumentNullException("User cannot be null", nameof(user));
            Amount = amount;
            Date = date;

            m_counter++;
        }

        public int Id { get; }
        public User User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }

        public virtual void Execute()
        {
            if (User == null)
            {
                throw new NullReferenceException("Transaction user cannot be null");
            }

            User.Balance += Amount;
        }

        public override string ToString()
        {
            return $"{Id}: {User.Username} {Amount} {Date}";
        }
    }
}
