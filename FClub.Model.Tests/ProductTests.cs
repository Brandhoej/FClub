using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class ProductTests
	{
		[TestInitialize]
		public void TestInitialize()
		{

		}

		[TestMethod]
		[DataRow(1, "ProductName", "10", true, true)]
		[DataRow(1, "ProductName", "10", false, true)]
		[DataRow(1, "ProductName", "10", true, false)]
		[DataRow(1, "ProductName", "10", false, false)]
		[DataRow(2, "ProductName", "10", true, true)]
		[DataRow(2, "ProductName", "10", false, true)]
		[DataRow(2, "ProductName", "10", true, false)]
		[DataRow(2, "ProductName", "10", false, false)]
		[DataRow(1, "ProductName", "-10", true, true)]
		[DataRow(1, "ProductName", "-10", false, true)]
		[DataRow(1, "ProductName", "-10", true, false)]
		[DataRow(1, "ProductName", "-10", false, false)]
		[DataRow(2, "ProductName", "-10", true, true)]
		[DataRow(2, "ProductName", "-10", false, true)]
		[DataRow(2, "ProductName", "-10", true, false)]
		[DataRow(2, "ProductName", "-10", false, false)]
		public void User_CorrectConstructionOfUser_Constructed(int id, string name, string price, bool active, bool canBeBoughtOnCredit)
		{
			// Arrange
			decimal _price;
			Product _product;

			// Act
			_price = decimal.Parse(price);
			_product = new Product(id, name, _price, active, canBeBoughtOnCredit);

			// Assert
			Assert.AreEqual(id, _product.Id);
			Assert.AreEqual(name, _product.Name);
			Assert.AreEqual(_price, _product.Price);
			Assert.AreEqual(active, _product.Active);
			Assert.AreEqual(canBeBoughtOnCredit, _product.CanBeBoughtOnCredit);
		}

		[TestMethod]
		public void Id_ThrowException_IfIdLessThan1()
		{
			// Arrange
			Product _product;

			// Act
			void Test()
			{
				_product = new Product(0, "Product", 10M, true, true);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void Id_DoesNotThrowException_IfIdGreaterThanOrEquals1()
		{
			// Arrange
			Product _product;

			// Act
			try
			{
				_product = new Product(1, "Product", 10M, true, true);
			}
			catch
			{
				// Assert
				Assert.Fail();
			}
		}

		[TestMethod]
		public void ___()
		{
			// Arrange

			// Act

			// Assert
		}

		[TestCleanup]
		public void TestCleanup()
		{

		}
	}
}
