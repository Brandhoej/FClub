using FClub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.DAL.Tests
{
	[TestClass]
	public class HashsetFClubContextTests
	{
		[TestMethod]
		public void Transactions_ReturnsEmptyHashset_IfConstructed()
		{
			// Arrange
			HashsetFClubContext _hashsetFClubContext;

			// Act
			_hashsetFClubContext = new HashsetFClubContext();

			// Assert
			Assert.AreNotEqual(null, _hashsetFClubContext.Transactions);
			Assert.AreEqual(_hashsetFClubContext.Transactions is HashSet<Transaction>, true);
		}

		[TestMethod]
		public void Users_ReturnsEmptyHashset_IfConstructed()
		{
			// Arrange
			HashsetFClubContext _hashsetFClubContext;

			// Act
			_hashsetFClubContext = new HashsetFClubContext();

			// Assert
			Assert.AreNotEqual(null, _hashsetFClubContext.Users);
			Assert.AreEqual(_hashsetFClubContext.Users is HashSet<User>, true);
		}

		[TestMethod]
		public void Products_ReturnsEmptyHashset_IfConstructed()
		{
			// Arrange
			HashsetFClubContext _hashsetFClubContext;

			// Act
			_hashsetFClubContext = new HashsetFClubContext();

			// Assert
			Assert.AreNotEqual(null, _hashsetFClubContext.Products);
			Assert.AreEqual(_hashsetFClubContext.Products is HashSet<Product>, true);
		}
	}
}
