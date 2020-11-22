using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.DAL.Tests
{
	[TestClass]
	public class HashsetUnitOfWorkTests
	{
		[TestMethod]
		public void HashsetUnitOfWork_SuccessfullyConstruct_True()
		{
			// Arrange
			IFClubContext _hashsetFClubContext;
			HashsetUnitOfWork _hashsetUnitOfWork;

			// Act
			_hashsetFClubContext = new HashsetFClubContext();
			_hashsetUnitOfWork = new HashsetUnitOfWork(_hashsetFClubContext);

			// Assert
			Assert.AreNotEqual(null, _hashsetUnitOfWork.Transactions);
			Assert.AreNotEqual(null, _hashsetUnitOfWork.Users);
			Assert.AreNotEqual(null, _hashsetUnitOfWork.Products);
		}

		[TestMethod]
		public void HashsetUnitOfWork_ThrowsArgumentNullException_IfContextIsNull()
		{
			// Arrange
			HashsetUnitOfWork _hashsetUnitOfWork;

			// Act
			void Test()
			{
				_hashsetUnitOfWork = new HashsetUnitOfWork(null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}
	}
}
