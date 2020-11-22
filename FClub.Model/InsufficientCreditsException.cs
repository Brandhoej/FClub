using System;
using System.Runtime.Serialization;

namespace FClub.Model
{
	public class InsufficientCreditsException : Exception
	{
		public InsufficientCreditsException(User user, Product product)
			: base($"Insufficient credit for {user} to buy {product}")
		{
			User = user;
			Product = product;
		}

		public InsufficientCreditsException(User user, Product product, string message)
			: base($"Insufficient credit for {user} to buy {product} - {message}")
		{
			User = user;
			Product = product;
		}

		public InsufficientCreditsException(User user, Product product, string message, Exception innerException)
			: base($"Insufficient credit for {user} to buy {product} - {message}", innerException)
		{
			User = user;
			Product = product;
		}

		protected InsufficientCreditsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }

		public User User { get; }
		public Product Product { get; }
	}
}
