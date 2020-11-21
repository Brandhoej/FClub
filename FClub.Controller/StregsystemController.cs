using FClub.BLL;
using FClub.Model;
using FClub.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Controller
{
	public interface IStregsystemController
	{

	}

	public class StregsystemController : IStregsystemController
	{
		private readonly IStregsystemUI _stregsystemUI;
		private readonly IStregsystem _stregsystem;

		public StregsystemController(IStregsystemUI stregsystemUI, IStregsystem stregsystem)
		{
			_stregsystemUI = stregsystemUI;
			_stregsystem = stregsystem;

			_stregsystemUI.CommandEntered += StregsystemUI_CommandEntered;
		}

		private void StregsystemUI_CommandEntered(string command)
		{
			User _user = new User(1, "Andreas", "Brandhoej", "Username", "akbr18@student.aau.dk", 100);
			Product _product = new Product(1, "Product1", 10, true, false);
			BuyTransaction _buyTransaction = new BuyTransaction(1, _user, _product, DateTime.Now);

			switch (command)
			{
				case "a": _stregsystemUI.DisplayUserInfo(_user); break;
				case "b": _stregsystemUI.DisplayUserBuysProduct(_buyTransaction); break;
				case "c": throw new NotImplementedException(); break;
				case "d": _stregsystemUI.DisplayUserNotFound("TestUser"); break;
				case "e": _stregsystemUI.DisplayProductNotFound("TestProduct"); break;
				case "f": _stregsystemUI.DisplayInsufficientCash(_user, _product); break;
				case "g": _stregsystemUI.DisplayGeneralError("General error"); break;
				case "h": _stregsystemUI.DisplayTooManyArgumentsError("Command with a lot of arguments"); break;
				case "i": _stregsystemUI.DisplayAdminCommandNotFoundMessage("Not found command"); break;
				default: _stregsystemUI.Close(); break;
			}
		}
	}
}
