using FClub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class BuyTransactionTests
	{
		private IIdentifier m_identifier;

		[TestInitialize]
		public void TestInitialize()
		{
			m_identifier = new Incrementalidentifier();
		}

		[TestMethod]
		public void BuyTransaction1_SetsAllProperpertiesCorrectly_Constructed()
		{
			// Arrange
			const decimal _productPrice = 10;
			const decimal _userInitBalance = 100;
			User _user;
			Product _product;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			_product = new Product(1, "Name", _productPrice, true, true);
			_buyTransaction = new BuyTransaction(m_identifier, _user, _product);

			// Assert
			Assert.AreEqual(_user, _buyTransaction.User);
			Assert.AreEqual(_product, _buyTransaction.Product);
			Assert.AreEqual(-_productPrice, _buyTransaction.Amount);
		}

		[TestMethod]
		public void BuyTransaction1_ThrowsArgumentNullException_IfProductIsNull()
		{
			// Arrange
			const decimal _userInitBalance = 100;
			User _user;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			void Test()
			{
				_buyTransaction = new BuyTransaction(m_identifier, _user, null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void BuyTransaction2_SetsAllProperpertiesCorrectly_Constructed()
		{
			// Arrange
			const decimal _productPrice = 10;
			const decimal _userInitBalance = 100;
			User _user;
			Product _product;
			DateTime _datetime = DateTime.Now;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			_product = new Product(1, "Name", _productPrice, true, true);
			_buyTransaction = new BuyTransaction(m_identifier, _user, _product, _datetime);

			// Assert
			Assert.AreEqual(_user, _buyTransaction.User);
			Assert.AreEqual(_product, _buyTransaction.Product);
			Assert.AreEqual(-_productPrice, _buyTransaction.Amount);
			Assert.AreEqual(_datetime, _buyTransaction.Date);
		}

		[TestMethod]
		public void BuyTransaction2_ThrowsArgumentNullException_IfProductIsNull()
		{
			// Arrange
			const decimal _userInitBalance = 100;
			User _user;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			void Test()
			{
				_buyTransaction = new BuyTransaction(m_identifier, _user, null, DateTime.Now);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void BuyTransaction_ThrowsArgumentNullException_IfUserIsNull()
		{
			// Arrange
			const decimal _productPrice = 10;
			Product _product;
			BuyTransaction _buyTransaction;

			// Act
			_product = new Product(1, "Name", _productPrice, true, true);
			void Test()
			{
				_buyTransaction = new BuyTransaction(m_identifier, null, _product);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void Execute_UsersBalanceDeductedPriceOfProduct_IfUserHasSufficientBalance()
		{
			// Arrange
			const decimal _productPrice = 10;
			const decimal _userInitBalance = 100;
			User _user;
			Product _product;
			DateTime _datetime = DateTime.Now;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			_product = new Product(1, "Name", _productPrice, true, true);
			_buyTransaction = new BuyTransaction(m_identifier, _user, _product, _datetime);
			_buyTransaction.Execute();

			// Assert
			Assert.AreEqual(_userInitBalance - _productPrice, _user.Balance);
		}

		[TestMethod]
		public void BuyTransaction_ConstructsSuccessfully_IfEverythingIsSetCorrectly()
		{
			// Arrange
			const int _id = 1;
			const decimal _productPrice = 10;
			const decimal _userInitBalance = 100;
			User _user;
			Product _product;
			DateTime _datetime = DateTime.Now;
			BuyTransaction _buyTransaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			_product = new Product(1, "Name", _productPrice, true, true);
			_buyTransaction = new BuyTransaction(_id, _user, _product, _datetime);

			// Assert
			Assert.AreEqual(_id, _buyTransaction.Id);
			Assert.AreEqual(_user, _buyTransaction.User);
			Assert.AreEqual(_product, _buyTransaction.Product);
			Assert.AreEqual(_datetime, _buyTransaction.Date);
		}

		[TestMethod]
		public void BuyTransaction_ThrowsArgumentNullException_IfProductIsNull()
		{
			// Arrange
			const int _id = 1;
			const decimal _userInitBalance = 100;
			User _user;
			Product _product = null;
			DateTime _datetime = DateTime.Now;
			BuyTransaction _buyTransaction;

			// Act
			void Test()
			{
				_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
				_buyTransaction = new BuyTransaction(_id, _user, _product, _datetime);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}
	}
}
