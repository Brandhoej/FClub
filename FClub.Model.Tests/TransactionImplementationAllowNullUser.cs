using System;

namespace FClub.Model.Tests
{
	internal class TransactionImplementationAllowNullUser : Transaction
	{
		public TransactionImplementationAllowNullUser(User user, decimal amount)
			: this(user, amount, DateTime.Now)
		{ }

		public TransactionImplementationAllowNullUser(User user, decimal amount, DateTime date)
			: this(0, user, amount, date)
		{ }

		public TransactionImplementationAllowNullUser(int id, User user, decimal amount, DateTime date)
			: base(id, user, amount, date)
		{ }
	}
}
