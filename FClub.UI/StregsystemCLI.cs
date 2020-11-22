using FClub.Model;
using FClub.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FClub.UI
{
	public class StregsystemCLI : IStregsystemUI
	{
		public event stregsystemEvent CommandEntered;

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

		public void DisplayProducts(IEnumerable<Product> products)
		{
			Console.Clear();

			int[] columnWidths = new int[3]
			{
				2, 4, 5
			};

			foreach (Product product in products)
			{
				columnWidths[0] = Math.Max(product.Id.ToString().Length, columnWidths[0]);
				columnWidths[1] = Math.Max(product.Name.ToString().Length, columnWidths[1]);
				columnWidths[2] = Math.Max(product.Price.ToString().Length, columnWidths[2]);
			}

			string format = "{0,-" + columnWidths[0] + "}   {1,-" + columnWidths[1] + "}   {2,-" + columnWidths[2] + "}";
			Console.WriteLine(string.Format(format, "Id", "Name", "Price"));
			foreach (Product product in products)
			{
				Console.WriteLine(string.Format(format, product.Id, product.Name, product.Price));
			}

			Console.WriteLine(string.Empty);
			Console.Write("Quickbuy> ");
		}

		public void DisplayUserBuysProduct(BuyTransaction transaction)
		{
			Console.Clear();

			Console.WriteLine($"'{transaction.User.Username}' has bought '{transaction.Product.Name}' at a price of '{transaction.Product.Price}'");
		}

		public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
		{
			Console.Clear();

			Console.WriteLine($"'{transaction.User.Username}' has bought '{transaction.Product.Name}' at a price of '{transaction.Product.Price} {count}' time(s)");
		}

		public void DisplayUserInfo(User user)
		{
			Console.Clear();

			Console.WriteLine($"User: '{user}'");
			Console.WriteLine($"Balance: '{user.Balance}'");
		}

		public void DisplayInsufficientCash(User user, Product product)
		{
			Console.Clear();

			Console.WriteLine($"'{user}' has insufficient funds for '{product.Name}' price: '{product.Price}'");
		}

		public void DisplayProductNotFound(string product)
		{
			Console.Clear();

			Console.WriteLine($"Product '{product}' not found");
		}

		public void DisplayTooManyArgumentsError(string command)
		{
			Console.Clear();

			Console.WriteLine($"Too many arguments in '{command}'");
		}

		public void DisplayAdminCommandNotFoundMessage(string adminCommand)
		{
			Console.Clear();

			Console.WriteLine($"Command not found '{adminCommand}'");
		}

		public void DisplayGeneralError(string errorString)
		{
			Console.Clear();

			Console.WriteLine($"Error: '{errorString}'");
		}

		public void DisplayUserNotFound(string username)
		{
			Console.Clear();

			Console.WriteLine($"User '{username}' not found");
		}

		public void Stop()
		{
			Running = false;
		}

		public void DisplayProduct(Product product)
		{
			Console.Clear();

			Console.WriteLine($"Product: '{product.Name}', Active: '{product.Active}', Price: '{product.Price}' CanBeBoughtOnCredit: '{product.CanBeBoughtOnCredit}'");
		}
	}
}
