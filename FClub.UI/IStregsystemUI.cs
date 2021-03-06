﻿using FClub.Core;
using FClub.Model;
using System.Collections.Generic;

namespace FClub.UI
{
	public interface IStregsystemUI
	{
		bool Running { get; }

		void Start();

		void DisplayProducts(IEnumerable<Product> products);
		void DisplayUserBuyInterface(User user, IEnumerable<Product> products);
		void DisplayUserInformation(User user, IEnumerable<Transaction> transactions);
		void DisplayUserBuysProduct(BuyTransaction transaction);
		void DisplayUserBuysProduct(int count, BuyTransaction transaction);
		
		void DisplayUserNotFound(string username);
		void DisplayProductNotFound(string product);

		void DisplayInsufficientCash(User user, Product product);
		void DisplayGeneralError(string errorString);
		void DisplayTooManyArgumentsError(string command);
		void DisplayAdminCommandNotFoundMessage(string adminCommand);

		void Stop();

		event stregsystemEvent CommandEntered;
	}
}
