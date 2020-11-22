using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FClub.Model.Tests
{
	[TestClass]
	public class InsufficientCreditsExceptionTests
	{
		[TestMethod]
		public void InsufficientCreditsException_Construction1Success_IfValidArguments()
		{
			// Arrange
			User _user = new User(1, "Andreas", "Brandhoej", "Username", "akbr18@student.aau.dk", 100);
			Product _product = new Product(1, "Product", 10, true, false);
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(_user, _product);

			// Assert
			Assert.AreEqual(_user, _insufficientCreditsException.User);
			Assert.AreEqual(_product, _insufficientCreditsException.Product);
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction3Success_IfValidArguments()
		{
			// Arrange
			User _user = new User(1, "Andreas", "Brandhoej", "Username", "akbr18@student.aau.dk", 100);
			Product _product = new Product(1, "Product", 10, true, false);
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(_user, _product, string.Empty);

			// Assert
			Assert.AreEqual(_user, _insufficientCreditsException.User);
			Assert.AreEqual(_product, _insufficientCreditsException.Product);
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction5Success_IfValidArguments()
		{
			// Arrange
			User _user = new User(1, "Andreas", "Brandhoej", "Username", "akbr18@student.aau.dk", 100);
			Product _product = new Product(1, "Product", 10, true, false);
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(_user, _product, string.Empty, null);

			// Assert
			Assert.AreEqual(_user, _insufficientCreditsException.User);
			Assert.AreEqual(_product, _insufficientCreditsException.Product);
		}
	}
}
