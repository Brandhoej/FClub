using FClub.Model;
using FClub.BLL;
using System;

namespace FClub.CLI
{
	public class StregsystemCLI : IStregsystemUI
	{
		public event stregsystemEvent CommandEntered;

		public StregsystemCLI(IStregsystem stregsystem)
		{
			Stregsystem = stregsystem ?? throw new ArgumentNullException(nameof(stregsystem), "Stregsystem required for the CLI");
		}

		private IStregsystem Stregsystem { get; }
		public bool Running { get; private set; }
		
		public void Start()
		{
			Run();
		}

		private void Run()
		{
			Running = true;
			while (Running)
			{
				string inputLine = Console.ReadLine();
				CommandEntered?.Invoke(inputLine);
			}
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			Console.WriteLine($"{transaction.User.Username} has bought {transaction.Product.Name} at a price of {transaction.Product.Price}");
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			// I dont know what this function is supposed to do. Maybe show "count" amount of transactions?
			// Rename from "DisplayUserBuysProduct" to "DisplayUserBuyTransactions(string username, int count)"?
			throw new NotImplementedException();
		}

		public void DisplayUserInfo(User user)
		{
			Console.WriteLine($"User: {user}");
		}

		#region Errors
		public void DisplayInsufficientCash(User user, Product product)
		{
			Console.WriteLine($"{user} has insufficient funds for {product.Name} price: {product.Price}");
		}

		public void DisplayProductNotFound(string product)
		{
			Console.WriteLine($"Product {product} not found");
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			Console.WriteLine($"Too many arguments in {command}");
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			Console.WriteLine($"Command not found {adminCommand}");
		}

		public void DisplayGeneralError(string errorString)
		{
			Console.WriteLine($"Error: {errorString}");
		}

		public void DisplayUserNotFound(string username)
		{
			Console.WriteLine($"User {username} not found");
		}
		#endregion

		public void Close()
		{
			Running = false;
		}
	}
}
