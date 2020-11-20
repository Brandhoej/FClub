using FClub.Model;
using System.Text;

namespace FClub.DAL
{
	public interface IUnitOfWork
	{
		IRepository<Transaction> Transactions { get; }
		IRepository<User> Users { get; }
		IRepository<Product> Products { get; }
	}
}
