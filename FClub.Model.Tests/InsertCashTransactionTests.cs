using FClub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class InsertCashTransactionTests
	{
		private IIdentifier m_identifier;

		[TestInitialize]
		public void TestInitialize()
		{
			m_identifier = new Incrementalidentifier();
		}

		[TestMethod]
		public void InsertCashTransaction_ConstructSuccessfully_Constructed()
		{
			// Arrange
			const decimal _amount = 0M;
			User _user = new User(m_identifier, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk");
			InsertCashTransaction _insertCashTransaction;

			// Act
			_insertCashTransaction = new InsertCashTransaction(m_identifier, _user, _amount);

			// Assert
			Assert.AreEqual(_user, _insertCashTransaction.User);
			Assert.AreEqual(_amount, _insertCashTransaction.Amount);
		}

		[TestMethod]
		public void InsertCashTransaction_ThrowsException_IfAmountIsLessThan0()
		{
			// Arrange
			const decimal _amount = -1M;
			User user = new User(m_identifier, "Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk");
			InsertCashTransaction _insertCashTransaction;

			// Act
			void Test()
			{
				_insertCashTransaction = new InsertCashTransaction(m_identifier, user, _amount);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}
	}
}
