using FClub.Model;
using System;
using System.Collections.Generic;

namespace FClub.DAL
{
	public class HashsetFClubContext : IFClubContext
	{
		public IEnumerable<Transaction> Transactions => new HashSet<Transaction>();
		public IEnumerable<User> Users => new HashSet<User>();
		public IEnumerable<Product> Products => new HashSet<Product>();
	}
}
