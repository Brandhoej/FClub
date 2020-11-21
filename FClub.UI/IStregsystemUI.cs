using FClub.Model;
using System.Collections.Generic;
using System.Text;

namespace FClub.UI
{
	public delegate void stregsystemEvent(string command);

	public interface IStregsystemUI
	{
		bool Running { get; }

		void Start();

		void DisplayUserInfo(User user);
		void DisplayUserBuysProduct(BuyTransaction transaction);
		void DisplayUserBuysProduct(int count, BuyTransaction transaction);
		
		void DisplayUserNotFound(string username);
		void DisplayProductNotFound(string product);

		void DisplayInsufficientCash(User user, Product product);
		void DisplayGeneralError(string errorString);
		void DisplayTooManyArgumentsError(string command);
		void DisplayAdminCommandNotFoundMessage(string adminCommand);

		void Close();

		event stregsystemEvent CommandEntered;
	}
}
