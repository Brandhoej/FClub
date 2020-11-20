using Microsoft.VisualStudio.TestTools.UnitTesting;
using FClub.Core;
using System;

namespace FClub.Core.Tests
{
	[TestClass]
	public class IncrementalidentifierTests
	{
		[TestMethod]
		public void Incrementalidentifier1_SuccessfullySetsMembers_True()
		{
			// Arrange
			Incrementalidentifier incrementalidentifier;

			// Act
			incrementalidentifier = new Incrementalidentifier();

			// Assert
			Assert.AreEqual(0, incrementalidentifier.Current);
		}

		[TestMethod]
		public void Incrementalidentifier2_SuccessfullySetsMembers_True()
		{
			// Arrange
			const int _start = 10;
			Incrementalidentifier _incrementalidentifier;

			// Act
			_incrementalidentifier = new Incrementalidentifier(_start);

			// Assert
			Assert.AreEqual(_start, _incrementalidentifier.Current);
		}

		[TestMethod]
		public void GetNextId_AddsOne_IfNoOverflow()
		{
			// Arrange
			const int _start = 10;
			Incrementalidentifier _incrementalidentifier;

			// Act
			_incrementalidentifier = new Incrementalidentifier(_start);

			// Assert
			for (int i = 0; i < 100; i++)
			{
				int last = _incrementalidentifier.GetNextId();
				Assert.AreEqual(1, _incrementalidentifier.GetNextId() - last);
			}
		}

		[TestMethod]
		public void GetNextId_ThrowsOverflowException_IfOverflow()
		{
			// Arrange
			const int _start = int.MaxValue;
			Incrementalidentifier _incrementalidentifier;

			// Act
			_incrementalidentifier = new Incrementalidentifier(_start);
			void Test()
			{
				_incrementalidentifier.GetNextId();
			}

			Assert.ThrowsException<OverflowException>(Test);
		}
	}
}
