using FClub.Core;
using System;

namespace FClub.Model
{
	public class InsertCashTransaction : Transaction
	{
		public InsertCashTransaction(IIdentifier identifier, User user, decimal amount)
			: base(identifier,
				   user ?? throw new ArgumentNullException(nameof(user),"User cannot be null"),
				   amount)
		{
			if (amount < 0)
			{
				throw new ArgumentException("Cannot insert negative amount", nameof(amount));
			}
		}

		public override string ToString()
		{
			return $"{Id} ({GetType().Name}) inserted {Amount} to {User.Username} {Date}";
		}
	}
}
