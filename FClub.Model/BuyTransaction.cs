using FClub.Core;
using System;

namespace FClub.Model
{
	public class BuyTransaction : Transaction
	{
		public BuyTransaction(IIdentifier identifier, User user, Product product)
			: this(identifier,
				   user,
				   product ?? throw new ArgumentNullException(nameof(product), "Product cannot be null"),
				   DateTime.Now)
		{ }

		public BuyTransaction(IIdentifier identifier, User user, Product product, DateTime date)
			: this(identifier,
				   user,
				   product == null ? throw new ArgumentNullException(nameof(product), "Product cannot be null") : product.Price,
				   date)
		{
			Product = product;
		}

		public BuyTransaction(int id, User user, Product product, DateTime date)
			: base(id,
				   user,
				   product == null ? throw new ArgumentNullException(nameof(product), "Product cannot be null") : -product.Price,
				   date)
		{
			Product = product;
		}


		protected BuyTransaction(IIdentifier identifier, User user, decimal price, DateTime date)
			: base(identifier, user, -price, date)
		{ }

		public Product Product { get; }
	}
}
