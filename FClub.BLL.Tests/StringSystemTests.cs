using FClub.Core;
using FClub.DAL;
using FClub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.BLL.Tests
{
	[TestClass]
	public class StringSystemTests
	{
		[TestMethod]
		public void StringSystem_ConstructionSucessfully_True()
		{
			// Arrange
			IFClubContext _fClubContext;
			IUnitOfWork _unitOfWork;
			IStringSystem _stringSystem;

			// Act
			_fClubContext = new HashsetFClubContext();
			_unitOfWork = new HashsetUnitOfWork(_fClubContext);
			new StringSystem(_unitOfWork);
		}

		[TestMethod]
		public void StringSystem_ThrowsArgumentNullException_IfProductsAreNull()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;

			// Act
			void Test()
			{
				_products = null;
				_users = new CollectionRepository<User>(new HashSet<User>());
				_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
				_identifier = new Incrementalidentifier();
				new StringSystem(_products, _users, _transactions, _identifier);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void StringSystem_ThrowsArgumentNullException_IfUserRepositoryAreNull()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;

			// Act
			void Test()
			{
				_products = new CollectionRepository<Product>(new HashSet<Product>());
				_users = null;
				_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
				_identifier = new Incrementalidentifier();
				new StringSystem(_products, _users, _transactions, _identifier);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void StringSystem_ThrowsArgumentNullException_IfTransactionRepositoryAreNull()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;

			// Act
			void Test()
			{
				_products = new CollectionRepository<Product>(new HashSet<Product>());
				_users = new CollectionRepository<User>(new HashSet<User>());
				_transactions = null;
				_identifier = new Incrementalidentifier();
				new StringSystem(_products, _users, _transactions, _identifier);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void StringSystem_ThrowsArgumentNullException_IfIdentifierIsNull()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;

			// Act
			void Test()
			{
				_products = new CollectionRepository<Product>(new HashSet<Product>());
				_users = new CollectionRepository<User>(new HashSet<User>());
				_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
				_identifier = null;
				new StringSystem(_products, _users, _transactions, _identifier);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void ActiveProducts_ReturnsActiveProducts_True()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStringSystem _stringSystem;

			// Act
			_products = new CollectionRepository<Product>(new HashSet<Product>()
			{
				new Product(1, "Product1", 0, false, false),
				new Product(2, "Product2", 0, false, true),
				new Product(3, "Product3", 0, true, false),
				new Product(4, "Product4", 0, true, true),
				new SeasonalProduct(5, "Product5", 0, false, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)),
				new SeasonalProduct(6, "Product6", 0, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2)),
				new SeasonalProduct(7, "Product7", 0, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)),
				new SeasonalProduct(8, "Product8", 0, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(5))
			});
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new StringSystem(_products, _users, _transactions, _identifier);
			IEnumerable<Product> _activeProducts = _stringSystem.ActiveProducts;

			// Assert
			Assert.AreEqual(4, _activeProducts.Count());
			Assert.AreEqual(false, _activeProducts.Any(curr => curr.Id == 1));
			Assert.AreEqual(false, _activeProducts.Any(curr => curr.Id == 2));
			Assert.AreEqual(true, _activeProducts.Any(curr => curr.Id == 3));
			Assert.AreEqual(true, _activeProducts.Any(curr => curr.Id == 4));
			Assert.AreEqual(true, _activeProducts.Any(curr => curr.Id == 5));
			Assert.AreEqual(true, _activeProducts.Any(curr => curr.Id == 6));
			Assert.AreEqual(false, _activeProducts.Any(curr => curr.Id == 7));
			Assert.AreEqual(false, _activeProducts.Any(curr => curr.Id == 8));
		}

		[TestMethod]
		public void AddCreditsToAccount_IncreasesBalanceOfUser_True()
		{
			// Arrange
			const decimal _startBalance = 100;
			const decimal _transactionBalance = 10;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStringSystem _stringSystem;

			// Act
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new StringSystem(_products, _users, _transactions, _identifier);
			_stringSystem.AddCreditsToAccount(_user, _transactionBalance);

			// Assert
			Assert.AreEqual(_startBalance + _transactionBalance, _user.Balance);
		}

		[TestMethod]
		public void BuyProduct_IncreasesBalanceOfUser_True()
		{
			// Arrange
			const decimal _startBalance = 100;
			Product _product;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStringSystem _stringSystem;

			// Act
			_product = new Product(1, "Name", 10, true, true);
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new StringSystem(_products, _users, _transactions, _identifier);
			_stringSystem.BuyProduct(_user, _product);

			// Assert
			Assert.AreEqual(_startBalance - _product.Price, _user.Balance);
		}

		[TestMethod]
		public void GetProductByID_FindsProduct_IfProductIdIsInProductRepository()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStringSystem _stringSystem;

			// Act
			_products = new CollectionRepository<Product>(new HashSet<Product>()
			{
				new Product(1, "Product1", 0, false, false),
				new Product(2, "Product2", 0, false, true),
				new Product(3, "Product3", 0, true, false),
				new Product(4, "Product4", 0, true, true),
				new SeasonalProduct(5, "Product5", 0, false, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)),
				new SeasonalProduct(6, "Product6", 0, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2)),
				new SeasonalProduct(7, "Product7", 0, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)),
				new SeasonalProduct(8, "Product8", 0, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(5))
			});
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new StringSystem(_products, _users, _transactions, _identifier);

			// Assert
			Assert.AreEqual("Product1", _stringSystem.GetProductById(1).Name);
			Assert.AreEqual("Product2", _stringSystem.GetProductById(2).Name);
			Assert.AreEqual("Product3", _stringSystem.GetProductById(3).Name);
			Assert.AreEqual("Product4", _stringSystem.GetProductById(4).Name);
			Assert.AreEqual("Product5", _stringSystem.GetProductById(5).Name);
			Assert.AreEqual("Product6", _stringSystem.GetProductById(6).Name);
			Assert.AreEqual("Product7", _stringSystem.GetProductById(7).Name);
			Assert.AreEqual("Product8", _stringSystem.GetProductById(8).Name);
		}

		[TestMethod]
		public void GetTransactions_FindsTheUsersTransactionsInSortedOrder_IfUserHasAnyuTransactions()
		{
			// Arrange
			const decimal _startBalance = 1000;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStringSystem _stringSystem;
			User _user;
			Product _product1, _product2, _product3, _product4;
			IList<Transaction> _userTransactions;

			// Act
			_user = new User(1, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk", _startBalance);
			_product1 = new Product(1, "Product1", 10, false, false);
			_product2 = new Product(2, "Product2", 20, false, true);
			_product3 = new Product(3, "Product2", 30, false, true);
			_product4 = new Product(4, "Product2", 40, false, true);
			_products = new CollectionRepository<Product>(new HashSet<Product>()
			{
				_product1,
				_product2,
				_product3,
				_product4
			});
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new StringSystem(_products, _users, _transactions, _identifier);
			_stringSystem.BuyProduct(_user, _product1);
			_stringSystem.BuyProduct(_user, _product3);
			_stringSystem.BuyProduct(_user, _product4);
			_stringSystem.BuyProduct(_user, _product2);
			_userTransactions = new List<Transaction>(_stringSystem.GetTransactions(_user, 3));

			// Assert
			Assert.AreEqual(3, _userTransactions.Count);
			Assert.AreEqual(-10, _userTransactions[0].Amount);
			Assert.AreEqual(-30, _userTransactions[1].Amount);
			Assert.AreEqual(-40, _userTransactions[2].Amount);
			Assert.AreEqual(true, _userTransactions[0].Date < _userTransactions[1].Date);
			Assert.AreEqual(true, _userTransactions[1].Date < _userTransactions[2].Date);
		}
	}
}
