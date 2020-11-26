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
	public class StregsystemTests
	{
		[TestMethod]
		public void StringSystem_ConstructionSucessfully_True()
		{
			// Arrange
			IFClubContext _fClubContext;
			IUnitOfWork _unitOfWork;

			// Act
			_fClubContext = new HashsetFClubContext();
			_unitOfWork = new HashsetUnitOfWork(_fClubContext);
			new Stregsystem(_unitOfWork);
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
				new Stregsystem(_products, _users, _transactions, _identifier);
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
				new Stregsystem(_products, _users, _transactions, _identifier);
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
				new Stregsystem(_products, _users, _transactions, _identifier);
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
				new Stregsystem(_products, _users, _transactions, _identifier);
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
			IStregsystem _stringSystem;

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
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
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
			IStregsystem _stringSystem;

			// Act
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			_stringSystem.AddCreditsToAccount(_user, _transactionBalance);

			// Assert
			Assert.AreEqual(_startBalance + _transactionBalance, _user.Balance);
		}

		[TestMethod]
		public void AddCreditsToAccount_ThrowsArgumentNullException_IfUserIsNull()
		{
			// Arrange
			const decimal _startBalance = 100;
			const decimal _transactionBalance = 10;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_user = null;
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_stringSystem.AddCreditsToAccount(_user, _transactionBalance);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void AddCreditsToAccount_ThrowsArgumentException_IfAmountIsLessThanZero()
		{
			// Arrange
			const decimal _startBalance = 100;
			const decimal _transactionBalance = -10;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_stringSystem.AddCreditsToAccount(_user, _transactionBalance);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void BuyProduct_DecreasesBalanceOfUser_True()
		{
			// Arrange
			const decimal _startBalance = 100;
			Product _product;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_product = new Product(1, "Name", 10, true, true);
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			_stringSystem.BuyProduct(_user, _product);

			// Assert
			Assert.AreEqual(_startBalance - _product.Price, _user.Balance);
		}

		[TestMethod]
		public void BuyProduct_ThrowArgumentNullException_IfUserIsNull()
		{
			// Arrange
			const decimal _startBalance = 100;
			Product _product;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_product = new Product(1, "Name", 10, true, true);
			_user = null;
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_stringSystem.BuyProduct(_user, _product);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void BuyProduct_ThrowArgumentNullException_IfProductIsNull()
		{
			// Arrange
			const decimal _startBalance = 100;
			Product _product;
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_product = null;
			_user = new User(1, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _startBalance);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>());
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_stringSystem.BuyProduct(_user, _product);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void GetProductByID_FindsProduct_IfProductIdIsInProductRepository()
		{
			// Arrange
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

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
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);

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
			IStregsystem _stringSystem;
			User _user;
			Product _product1, _product2, _product3, _product4;
			IList<Transaction> _userTransactions;

			// Act
			_user = new User(1, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk", _startBalance);
			_product1 = new Product(1, "Product1", 10, true, false);
			_product2 = new Product(2, "Product2", 20, true, true);
			_product3 = new Product(3, "Product2", 30, true, true);
			_product4 = new Product(4, "Product2", 40, true, true);
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
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			_stringSystem.BuyProduct(_user, _product1);
			_stringSystem.BuyProduct(_user, _product3);
			_stringSystem.BuyProduct(_user, _product4);
			_stringSystem.BuyProduct(_user, _product2);
			_userTransactions = new List<Transaction>(_stringSystem.GetTransactions(_user, 3));

			// Assert
			Assert.AreEqual(3, _userTransactions.Count);
			Assert.AreEqual(-20, _userTransactions[0].Amount);
			Assert.AreEqual(-40, _userTransactions[1].Amount);
			Assert.AreEqual(-30, _userTransactions[2].Amount);
			Assert.AreEqual(true, _userTransactions[0].Date > _userTransactions[1].Date);
			Assert.AreEqual(true, _userTransactions[1].Date > _userTransactions[2].Date);
		}

		[TestMethod]
		public void GetTransactions_ThrowsArgumentNullException_IfUserIsNull()
		{
			// Arrange
			const decimal _startBalance = 1000;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;
			User _user;
			Product _product1, _product2, _product3, _product4;
			IList<Transaction> _userTransactions;

			// Act
			_user = null;
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
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_stringSystem.GetTransactions(_user, 10);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void GetUserByUsername_FindsUserByName_IfUserCouldBeFound()
		{
			// Arrange
			User _user;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;

			// Act
			_user = new User(1, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>()
			{
				_user
			});
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);

			// Assert
			Assert.AreEqual(_user, _stringSystem.GetUserByUsername("Hyw"));
		}

		[TestMethod]
		public void GetUsers2_FindsUserByName_IfUserCouldBeFound()
		{
			// Arrange
			User _user1, _user2, _user3, _user4, _user5;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;
			IEnumerable<User> _UserResult;

			// Act
			_user1 = new User(1, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user2 = new User(2, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user3 = new User(3, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user4 = new User(4, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user5 = new User(5, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>()
			{
				_user1,
				_user2,
				_user3,
				_user4,
				_user5
			});
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			_UserResult = _stringSystem.GetUsers(u => u.Id >= 2 && u.Id <= 4);

			// Assert
			Assert.AreEqual(3, _UserResult.Count());
			Assert.AreEqual(true, _UserResult.All(u => u.Id >= 2 && u.Id <= 4));
		}

		[TestMethod]
		public void GetUsers_ThrowsArgumentNullException_IfPredicateIsNull()
		{
			// Arrange
			User _user1, _user2, _user3, _user4, _user5;
			IRepository<Product> _products;
			IRepository<User> _users;
			IRepository<Transaction> _transactions;
			IIdentifier _identifier;
			IStregsystem _stringSystem;
			IEnumerable<User> _UserResult;

			// Act
			_user1 = new User(1, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user2 = new User(2, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user3 = new User(3, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user4 = new User(4, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_user5 = new User(5, "Andreas", "Bradnhoej", "Hyw", "akbr18@student.aau.dk", 0);
			_products = new CollectionRepository<Product>(new HashSet<Product>());
			_users = new CollectionRepository<User>(new HashSet<User>()
			{
				_user1,
				_user2,
				_user3,
				_user4,
				_user5
			});
			_transactions = new CollectionRepository<Transaction>(new HashSet<Transaction>());
			_identifier = new Incrementalidentifier();
			_stringSystem = new Stregsystem(_products, _users, _transactions, _identifier);
			void Test()
			{
				_UserResult = _stringSystem.GetUsers(null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void BuyProduct_ThrowsInsufficientCreditsException_IfUserDoesNotHaveBalanceForProduct()
		{
			// Arrange
			User _user;
			Product _product;
			IFClubContext _fClubContext;
			IUnitOfWork _unitOfWork;
			IStregsystem _stregsystem;

			// Act
			_fClubContext = new HashsetFClubContext();
			_unitOfWork = new HashsetUnitOfWork(_fClubContext);
			_stregsystem = new Stregsystem(_unitOfWork);
			_user = new User(1, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk", 10);
			_product = new Product(1, "product", 20, true, false);
			void Test()
			{
				_stregsystem.BuyProduct(_user, _product);
			}

			// Assert
			Assert.ThrowsException<InsufficientCreditsException>(Test);
		}
	}
}
