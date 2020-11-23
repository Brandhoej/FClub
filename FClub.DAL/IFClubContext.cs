using FClub.Model;
using System.Collections.Generic;
using System.Text;

namespace FClub.DAL
{
	public interface IFClubContext
	{
		IEnumerable<Transaction> Transactions { get; }
		IEnumerable<User> Users { get; }
		IEnumerable<Product> Products { get; }
	}
}
