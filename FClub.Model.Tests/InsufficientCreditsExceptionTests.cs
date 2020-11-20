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
		[TestInitialize]
		public void TestInitialize()
		{

		}

		[TestMethod]
		public void InsufficientCreditsException_Construction1Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			try
			{
				// Act
				_insufficientCreditsException = new InsufficientCreditsException(null, null);
			}
			catch
			{
				// Assert
				Assert.Fail();
			}
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction2Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			try
			{
				// Act
				_insufficientCreditsException = new InsufficientCreditsException(null, null);
			}
			catch
			{
				// Assert
				Assert.Fail();
			}
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction3Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			try
			{
				// Act
				_insufficientCreditsException = new InsufficientCreditsException(null, null, string.Empty);
			}
			catch
			{
				// Assert
				Assert.Fail();
			}
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction5Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			try
			{
				// Act
				_insufficientCreditsException = new InsufficientCreditsException(null, null, string.Empty, null);
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
