using FClub.DAL.IO;
using FClub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.DAL.Tests
{
	[TestClass]
	public class ProductsReaderTest
	{
		[TestMethod]
		public void ProductsReader_SuccessfullyConstructs_IfCorrectParameters()
		{
			// Arrange
			string _separator = "separator";
			string _path = "path";
			ProductsReader _productsReader;

			// Act
			_productsReader = new ProductsReader(_separator, _path);

			// Assert
			Assert.AreEqual(_separator, _productsReader.Separator);
			Assert.AreEqual(_path, _productsReader.Path);
		}

		[TestMethod]
		public void ProductsReader_ThrowsArgumentNullException_IfPathIsNUll()
		{
			// Arrange
			string _separator = "separator";
			string _path = null;
			ProductsReader _productsReader;

			// Act
			void Test()
			{
				_productsReader = new ProductsReader(_separator, _path);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void ProductsReader_ThrowsArgumentNullException_IfSeparatorIsNUll()
		{
			// Arrange
			string _separator = null;
			string _path = "path";
			ProductsReader _productsReader;

			// Act
			void Test()
			{
				_productsReader = new ProductsReader(_separator, _path);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void CreateProductFromLine_ThrowsArgumentException_IfInputHasLessThan6Separators()
		{
			// Arrange
			string _separator = ",";
			string _path = "path";
			ProductsReader _productsReader;

			// Act
			void Test()
			{
				_productsReader = new ProductsReader(_separator, _path);
				_productsReader.CreateProductFromLine(string.Empty);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void CreateProductFromLine1_ConstructsProductUserSuccessfully_IfCorrectInput()
		{
			// Arrange
			int _id = 1;
			string _name = "Product";
			decimal _price = 123;
			bool _active = true;
			string _separator = ",";
			string _path = "path";
			ProductsReader _productsReader;
			Product _product;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_name);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_price);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_active == true ? "1" : "0");

			_productsReader = new ProductsReader(_separator, _path);
			_product = _productsReader.CreateProductFromLine(_stringBuilder.ToString());

			// Assert
			Assert.AreEqual(_separator, _productsReader.Separator);
			Assert.AreEqual(_path, _productsReader.Path);
			Assert.AreEqual(_id, _product.Id);
			Assert.AreEqual(_name, _product.Name);
			Assert.AreEqual(_price / 100.0m, _product.Price);
			Assert.AreEqual(_active, _product.Active);
		}

		[TestMethod]
		public void CreateProductFromLine2_ConstructsProductUserSuccessfully_IfCorrectInput()
		{
			// Arrange
			int _id = 1;
			string _name = "Product";
			decimal _price = 123;
			bool _active = false;
			string _separator = ",";
			string _path = "path";
			ProductsReader _productsReader;
			Product _product;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_name);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_price);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_active == true ? "1" : "0");

			_productsReader = new ProductsReader(_separator, _path);
			_product = _productsReader.CreateProductFromLine(_stringBuilder.ToString());

			// Assert
			Assert.AreEqual(_separator, _productsReader.Separator);
			Assert.AreEqual(_path, _productsReader.Path);
			Assert.AreEqual(_id, _product.Id);
			Assert.AreEqual(_name, _product.Name);
			Assert.AreEqual(_price / 100.0m, _product.Price);
			Assert.AreEqual(_active, _product.Active);
		}

		[TestMethod]
		public void CreateProductFromLine3_ConstructsProductUserSuccessfully_IfCorrectInput()
		{
			// Arrange
			int _id = 1;
			string _name = "Product";
			decimal _price = 123;
			bool _active = true;
			string _separator = ",";
			string _path = "path";
			DateTime _date = new DateTime(2007, 10, 1, 16, 30, 1);
			string _dateTime = _date.ToString("yyyy-MM-dd HH:mm:ss");
			ProductsReader _productsReader;
			Product _product;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_name);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_price);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_active == true ? "1" : "0");
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_dateTime);

			_productsReader = new ProductsReader(_separator, _path);
			_product = _productsReader.CreateProductFromLine(_stringBuilder.ToString());

			// Assert
			Assert.AreEqual(_separator, _productsReader.Separator);
			Assert.AreEqual(_path, _productsReader.Path);
			Assert.AreEqual(_id, _product.Id);
			Assert.AreEqual(_name, _product.Name);
			Assert.AreEqual(_price / 100.0m, _product.Price);
			Assert.AreEqual(_date, (_product as SeasonalProduct).SeasonEndDate);
		}

		[TestMethod]
		public void CreateProductFromLine4_ConstructsProductUserSuccessfully_IfCorrectInput()
		{
			// Arrange
			int _id = 1;
			string _name = "Product";
			decimal _price = -123;
			bool _active = true;
			string _separator = ",";
			string _path = "path";
			DateTime _date = default;
			string _dateTime = _date.ToString("yyyy-MM-dd HH:mm:ss");
			ProductsReader _productsReader;
			Product _product;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_name);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_price);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_active == true ? "1" : "0");
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_dateTime);

			void Test()
			{
				_productsReader = new ProductsReader(_separator, _path);
				_product = _productsReader.CreateProductFromLine(_stringBuilder.ToString());
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}
	}
}
