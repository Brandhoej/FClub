using System;
using System.Collections.Generic;
using FClub.Model;

namespace FClub.BLL
{
	public interface IStregsystem
	{
		IEnumerable<Product> ActiveProducts { get; }

		InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
		BuyTransaction BuyProduct(User user, Product product);
		Product GetProductById(int id);
		IEnumerable<Transaction> GetTransactions(User user, int count);
		IEnumerable<User> GetUsers(Predicate<User> predicate);
		User GetUserByUsername(string username);

		event User.balanceNotification UserBalanceWarning;
	}
}
