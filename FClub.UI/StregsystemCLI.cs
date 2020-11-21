using FClub.Model;
using FClub.BLL;
using System;

namespace FClub.UI
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
			Console.WriteLine($"{transaction.User.Username} has bought {transaction.Product.Name} at a price of {transaction.Product.Price} {count} time(s)");
		}

		public void DisplayUserInfo(User user)
		{
			Console.WriteLine($"User: {user}");
		}

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

		public void Stop()
		{
			Running = false;
		}
	}
}
