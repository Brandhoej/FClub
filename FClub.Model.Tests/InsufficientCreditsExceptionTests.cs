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
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(null, null);
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction2Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(null, null);
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction3Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(null, null, string.Empty);
		}

		[TestMethod]
		public void InsufficientCreditsException_Construction5Success_IfValidArguments()
		{
			// Arrange
			InsufficientCreditsException _insufficientCreditsException;

			// Act
			_insufficientCreditsException = new InsufficientCreditsException(null, null, string.Empty, null);
		}
	}
}
