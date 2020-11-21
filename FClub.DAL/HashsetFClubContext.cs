using FClub.DAL.IO;
using FClub.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FClub.DAL
{
	public class HashsetFClubContext : IFClubContext
	{
		public HashsetFClubContext(string dataProductsPath, string dataUsersPath)
		{
			IProductsReader _productsReader = new ProductsReader(";", dataProductsPath);
			IUsersReader _usersReader = new UsersReader(",", dataUsersPath);

			Transactions = new HashSet<Transaction>();
			Products = new HashSet<Product>(_productsReader.ReadProducts());
			Users = new HashSet<User>(_usersReader.ReadUsers());
		}

		public HashsetFClubContext()
			: this(new HashSet<Transaction>(), new HashSet<User>(), new HashSet<Product>())
		{ }

		public HashsetFClubContext(IEnumerable<Transaction> transactions, IEnumerable<User> users, IEnumerable<Product> products)
		{
			Transactions = new HashSet<Transaction>(transactions);
			Users = new HashSet<User>(users);
			Products = new HashSet<Product>(products);
		}

		public IEnumerable<Transaction> Transactions { get; } = new HashSet<Transaction>();
		public IEnumerable<User> Users { get; } = new HashSet<User>();
		public IEnumerable<Product> Products { get; } = new HashSet<Product>();
	}
}
