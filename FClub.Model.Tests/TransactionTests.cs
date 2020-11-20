using FClub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class TransactionTests
	{
		private IIdentifier m_identifier;

		[TestInitialize]
		public void TestInitialize()
		{
			m_identifier = new Incrementalidentifier();
		}

		[TestMethod]
		public void Transaction1_SetsMembersCorrectly_IfInputGiven()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
			_transaction = new TransactionImplementation(m_identifier, _user, _amount);

			// Assert
			Assert.AreEqual(_user, _transaction.User);
			Assert.AreEqual(_amount, _transaction.Amount);
		}

		[TestMethod]
		public void Transaction2_SetsMembersCorrectly_IfInputGiven()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;
			DateTime _date = DateTime.Now.AddDays(1);

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
			_transaction = new TransactionImplementation(m_identifier, _user, _amount, _date);

			// Assert
			Assert.AreEqual(_user, _transaction.User);
			Assert.AreEqual(_amount, _transaction.Amount);
			Assert.AreEqual(_date.CompareTo(_transaction.Date), 0);
		}

		[TestMethod]
		public void Transaction_ThrowsArgumentNullException_IfUserIsNull()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;

			// Act
			void Test()
			{
				_user = null;
				_transaction = new TransactionImplementation(m_identifier, _user, _amount);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void Transaction_ThrowsArgumentNullException1_IfIdentifierIsNull()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;

			// Act
			void Test()
			{
				_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
				_transaction = new TransactionImplementation(null, _user, _amount);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void Transaction_ThrowsArgumentNullException2_IfIdentifierIsNull()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;

			// Act
			void Test()
			{
				_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
				_transaction = new TransactionImplementation(null, _user, _amount, DateTime.Now);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void Execute_ThrowsNullReferenceException_IfUserIsNull()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;

			// Act
			void Test()
			{
				_user = null;
				_transaction = new TransactionImplementationAllowNullUser(_user, _amount);
				_transaction.Execute();
			}

			// Assert
			Assert.ThrowsException<NullReferenceException>(Test);
		}

		[TestMethod]
		public void Execute_AddsAmountToUser_IfUserIsDefined()
		{
			// Arrange
			const decimal _transactionAmount = 10;
			const decimal _userInitBalance = 10;
			User _user;
			Transaction _transaction;

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk", _userInitBalance);
			_transaction = new TransactionImplementation(m_identifier, _user, _transactionAmount);
			_transaction.Execute();

			// Assert
			Assert.AreEqual(_transactionAmount + _userInitBalance, _user.Balance);
		}

		[TestMethod]
		public void ToString_DoesNotThrowException_True()
		{
			// Arrange
			const decimal _amount = 0;
			User _user;
			Transaction _transaction;
			DateTime _date = DateTime.Now.AddDays(1);

			// Act
			_user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
			_transaction = new TransactionImplementation(m_identifier, _user, _amount, _date);
			_transaction.ToString();
		}

		[TestMethod]
		public void Transaction_UniqueIdsWhichAreInOrderOfCreation_AllTransactions()
		{
			// Arrange
			const int _amount = 100;
			const decimal _transactionAmount = 0;
			User _user = new User(m_identifier, "FirstName", "LastName", "Username", "akbr18@student.aau.dk");
			IList<Transaction> _transactions = new List<Transaction>();

			// Act
			while (_transactions.Count < _amount)
			{
				Transaction _transaction = new TransactionImplementation(m_identifier, _user, _transactionAmount);
				_transactions.Add(_transaction);
			}

			// Assert
			for (int i = 1; i < _transactions.Count; i++)
			{
				// Since they all must be less than then we are both checking uniqueness and order
				Assert.AreEqual(_transactions[i - 1].Id < _transactions[i].Id, true);
			}
		}
	}
}
