using FClub.Model;
using System;
using System.Collections.Generic;

namespace FClub.DAL
{
	public class HashsetUnitOfWork : IUnitOfWork
	{
		private IRepository<Transaction> m_transactions;
		private IRepository<User> m_users;
		private IRepository<Product> m_products;

		public HashsetUnitOfWork(IFClubContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context), "Context must not be null");
		}

		public IRepository<Transaction> Transactions 
			=> m_transactions ??= new CollectionRepository<Transaction>(new HashSet<Transaction>(Context.Transactions));
		public IRepository<User> Users
			=> m_users ??= new CollectionRepository<User>(new HashSet<User>(Context.Users));
		public IRepository<Product> Products
			=> m_products ??= new CollectionRepository<Product>(new HashSet<Product>(Context.Products));

		private IFClubContext Context { get; }
	}
}
