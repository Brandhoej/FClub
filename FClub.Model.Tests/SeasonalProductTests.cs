using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class SeasonalProductTests
	{
		[TestInitialize]
		public void TestInitialize()
		{

		}

		[TestMethod]
		[DataRow(1, "ProductName", "10", true, true, 10, 100)]
		[DataRow(1, "ProductName", "10", false, true, 10, 100)]
		[DataRow(1, "ProductName", "10", true, false, 10, 100)]
		[DataRow(1, "ProductName", "10", false, false, 10, 100)]
		[DataRow(2, "ProductName", "10", true, true, 10, 100)]
		[DataRow(2, "ProductName", "10", false, true, 10, 100)]
		[DataRow(2, "ProductName", "10", true, false, 10, 100)]
		[DataRow(2, "ProductName", "10", false, false, 10, 100)]
		[DataRow(1, "ProductName", "-10", true, true, 10, 100)]
		[DataRow(1, "ProductName", "-10", false, true, 10, 100)]
		[DataRow(1, "ProductName", "-10", true, false, 10, 100)]
		[DataRow(1, "ProductName", "-10", false, false, 10, 100)]
		[DataRow(2, "ProductName", "-10", true, true, 10, 100)]
		[DataRow(2, "ProductName", "-10", false, true, 10, 100)]
		[DataRow(2, "ProductName", "-10", true, false, 10, 100)]
		[DataRow(2, "ProductName", "-10", false, false, 10, 100)]
		public void User_CorrectConstructionOfUser_Constructed(int id, string name, string price, bool active, bool canBeBoughtOnCredit, int unixSeasonStartDate, int unixSeasonEndDate)
		{
			// Arrange
			decimal _price;
			DateTime _seasonStartDate = new DateTime(unixSeasonStartDate);
			DateTime _seasonEndDate = new DateTime(unixSeasonEndDate);
			SeasonalProduct _product;

			// Act
			_price = decimal.Parse(price);
			_product = new SeasonalProduct(id, name, _price, active, canBeBoughtOnCredit, _seasonStartDate, _seasonEndDate);

			// Assert
			Assert.AreEqual(id, _product.Id);
			Assert.AreEqual(name, _product.Name);
			Assert.AreEqual(_price, _product.Price);
			Assert.AreEqual(active, _product.Active);
			Assert.AreEqual(canBeBoughtOnCredit, _product.CanBeBoughtOnCredit);
			Assert.AreEqual(_seasonStartDate, _product.SeasonStartDate);
			Assert.AreEqual(_seasonEndDate, _product.SeasonEndDate);
		}

		[TestMethod]
		[DataRow(1, "ProductName", "10", true, true, 100, 100)]
		[DataRow(1, "ProductName", "10", false, true, 100, 100)]
		[DataRow(1, "ProductName", "10", true, false, 100, 100)]
		[DataRow(1, "ProductName", "10", false, false, 100, 100)]
		[DataRow(2, "ProductName", "10", true, true, 100, 100)]
		[DataRow(2, "ProductName", "10", false, true, 100, 100)]
		[DataRow(2, "ProductName", "10", true, false, 100, 100)]
		[DataRow(2, "ProductName", "10", false, false, 100, 100)]
		[DataRow(1, "ProductName", "-10", true, true, 100, 100)]
		[DataRow(1, "ProductName", "-10", false, true, 100, 100)]
		[DataRow(1, "ProductName", "-10", true, false, 100, 100)]
		[DataRow(1, "ProductName", "-10", false, false, 100, 100)]
		[DataRow(2, "ProductName", "-10", true, true, 100, 100)]
		[DataRow(2, "ProductName", "-10", false, true, 100, 100)]
		[DataRow(2, "ProductName", "-10", true, false, 100, 100)]
		[DataRow(2, "ProductName", "-10", false, false, 100, 100)]
		public void User_ThrowsException_IfSeasonStartIsTheSameAsSeasonEnd(int id, string name, string price, bool active, bool canBeBoughtOnCredit, int unixSeasonStartDate, int unixSeasonEndDate)
		{
			// Arrange
			decimal _price;
			DateTime _seasonStartDate = new DateTime(unixSeasonStartDate);
			DateTime _seasonEndDate = new DateTime(unixSeasonEndDate);
			SeasonalProduct _product;

			// Act
			_price = decimal.Parse(price);
			void Test()
			{
				_product = new SeasonalProduct(id, name, _price, active, canBeBoughtOnCredit, _seasonStartDate, _seasonEndDate);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		[DataRow(1, "ProductName", "10", true, true, 101, 100)]
		[DataRow(1, "ProductName", "10", false, true, 101, 100)]
		[DataRow(1, "ProductName", "10", true, false, 101, 100)]
		[DataRow(1, "ProductName", "10", false, false, 101, 100)]
		[DataRow(2, "ProductName", "10", true, true, 101, 100)]
		[DataRow(2, "ProductName", "10", false, true, 101, 100)]
		[DataRow(2, "ProductName", "10", true, false, 101, 100)]
		[DataRow(2, "ProductName", "10", false, false, 101, 100)]
		[DataRow(1, "ProductName", "-10", true, true, 101, 100)]
		[DataRow(1, "ProductName", "-10", false, true, 101, 100)]
		[DataRow(1, "ProductName", "-10", true, false, 101, 100)]
		[DataRow(1, "ProductName", "-10", false, false, 101, 100)]
		[DataRow(2, "ProductName", "-10", true, true, 101, 100)]
		[DataRow(2, "ProductName", "-10", false, true, 101, 100)]
		[DataRow(2, "ProductName", "-10", true, false, 101, 100)]
		[DataRow(2, "ProductName", "-10", false, false, 101, 100)]
		public void User_ThrowsException_IfSeasonStartIsLaterThanSeasonEnd(int id, string name, string price, bool active, bool canBeBoughtOnCredit, int unixSeasonStartDate, int unixSeasonEndDate)
		{
			// Arrange
			decimal _price;
			DateTime _seasonStartDate = new DateTime(unixSeasonStartDate);
			DateTime _seasonEndDate = new DateTime(unixSeasonEndDate);
			SeasonalProduct _product;

			// Act
			_price = decimal.Parse(price);
			void Test()
			{
				_product = new SeasonalProduct(id, name, _price, active, canBeBoughtOnCredit, _seasonStartDate, _seasonEndDate);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void Id_ThrowException_IfIdLessThan1()
		{
			// Arrange
			SeasonalProduct _product;

			// Act
			void Test()
			{
				_product = new SeasonalProduct(0, "Product", 10M, true, true, DateTime.Now, DateTime.Now.AddDays(1));
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void Id_DoesNotThrowException_IfIdGreaterThanOrEquals1()
		{
			// Arrange
			SeasonalProduct _product;

			// Act
			try
			{
				_product = new SeasonalProduct(1, "Product", 10M, true, true, DateTime.Now, DateTime.Now.AddDays(1));
			}
			catch
			{
				// Assert
				Assert.Fail();
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{

		}
	}
}
