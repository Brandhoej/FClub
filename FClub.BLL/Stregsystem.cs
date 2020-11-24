using System;
using System.Collections.Generic;
using FClub.Model;
using FClub.DAL;
using FClub.Core;
using System.Linq;
using FClub.BLL.IO.Logging;
using System.IO;

namespace FClub.BLL
{
	public class Stregsystem : IStregsystem
	{
		public event User.balanceNotification UserBalanceWarning;

		private readonly ILogger<Transaction> m_transactionLogger;

		public Stregsystem(IUnitOfWork unitOfWork)
			: this(unitOfWork.Products, unitOfWork.Users, unitOfWork.Transactions)
		{ }

		public Stregsystem(IRepository<Product> products, IRepository<User> users, IRepository<Transaction> transactions)
			: this(products, users, transactions, new Incrementalidentifier())
		{ }

		public Stregsystem(IRepository<Product> products, IRepository<User> users, IRepository<Transaction> transactions, IIdentifier transactionIdentifier)
		{
			Products = products ?? throw new ArgumentNullException(nameof(Products), "Product repository cannot be null");
			Users = users ?? throw new ArgumentNullException(nameof(Users), "User repository cannot be null");
			Transactions = transactions ?? throw new ArgumentNullException(nameof(Transactions), "Transaction repository cannot be null");
			TransactionIdentifier = transactionIdentifier ?? throw new ArgumentNullException(nameof(transactionIdentifier), "Transaction identifier cannot be null");
			m_transactionLogger = new TransactionLogger(Path.Combine(Environment.CurrentDirectory, "Log"), "transaction_log.txt");

			// TODO: Remove
			Users.Insert(new User(99, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk", 100));
		}

		private IRepository<Product> Products { get; }
		private IRepository<User> Users { get; }
		private IRepository<Transaction> Transactions { get; }
		private IIdentifier TransactionIdentifier { get; }
		
		public IEnumerable<Product> ActiveProducts => Products.FindAll(curr => curr.Active);

		public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
		{
			if (amount < 1)
			{
				throw new ArgumentException($"{amount} is not valid as an amount to buy", nameof(amount));
			}

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "User cannot be null");
			}

			try
			{
				InsertCashTransaction _insertCashTransaction = new InsertCashTransaction(TransactionIdentifier, user, amount);
				ExecuteTransaction(_insertCashTransaction);
				return _insertCashTransaction;
			}
			catch 
			{
				throw;
			}
		}

		public BuyTransaction BuyProduct(User user, Product product, int amount = 1)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "User cannot be null");
			}

			if (product == null)
			{
				throw new ArgumentNullException(nameof(product), "Product cannot be null");
			}

			if (!product.Active)
			{
				throw new ArgumentException("Product is inactive", nameof(product));
			}

			if (!product.CanBeBoughtOnCredit &&
				product.Price * amount > user.Balance)
			{
				throw new InsufficientCreditsException(user, product);
			}

			try
			{
				BuyTransaction _buyTransaction = default;
				for (int i = 0; i < amount; i++)
				{
					_buyTransaction = new BuyTransaction(TransactionIdentifier, user, product);
					ExecuteTransaction(_buyTransaction);
				}
				return _buyTransaction;
			}
			catch
			{
				throw;
			}
		}

		public Product GetProductById(int id)
		{
			return Products.Find(curr => curr.Id == id);
		}

		public IEnumerable<Transaction> GetTransactions(User user, int count)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "User cannot be null");
			}

			return Transactions
				.FindAll(transaction => transaction.User == user)
				.OrderBy(transaction => transaction.Date)
				.Take(count);
		}

		public User GetUserByUsername(string username)
		{
			return Users.Find(curr => curr.Username == username);
		}

		public IEnumerable<User> GetUsers(Predicate<User> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
			}

			return Users.FindAll(predicate);
		}

		private void ExecuteTransaction(Transaction transaction)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException(nameof(transaction), "Transaction to execute cannot be null");
			}

			try
			{
				transaction.User.OnBalanceNotification += UserBalanceWarning;
				transaction.Execute();
				m_transactionLogger.Log(transaction);
				Transactions.Insert(transaction);
			}
			finally
			{
				transaction.User.OnBalanceNotification -= UserBalanceWarning;
			}
		}
	}
}
