using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FClub.DAL.Tests
{
	[TestClass]
	public class CollectionRepositoryTests
	{
		[TestMethod]
		public void CollectionRepository_SetsCollectionProperty_IfGivenInConstructor()
		{
			// Arrange
			ICollection<int> _collection = new List<int>();
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);

			// Assert
			Assert.AreEqual(_collection, _repository.Collection);
		}

		[TestMethod]
		public void CollectionRepository_ThrowsArgumentNullException_IdCollectionNotGivenInConstructor()
		{
			// Act
			void Test()
			{
				new CollectionRepository<int>(null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void Insert_AddsItemToCollection_IfItIsNotFoundAlready()
		{
			// Arrange
			const int _toInsert = 4;
			ICollection<int> _collection = new List<int>()
			{ 
				1, 
				2, 
				3 
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);

			// Assert
			Assert.AreEqual(_repository.Insert(_toInsert), _toInsert);
		}

		[TestMethod]
		public void Insert_ThrowsArgumentException_IfItIsNotFoundAlready()
		{
			// Arrange
			const int _toInsert = 2;
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			void Test()
			{
				_repository.Insert(_toInsert);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void Delete_ReturnsTheItemWhenDeleted_IfTheItemWasFound()
		{           
			// Arrange
			const int _toDelete = 2;
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);

			// Assert
			Assert.AreEqual(_repository.Delete(_toDelete), _toDelete);
		}

		[TestMethod]
		public void Delete_ReturnsDefault_IfTheItemWasNotFound()
		{
			// Arrange
			const int _toDelete = default;
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);

			// Assert
			Assert.AreEqual(_repository.Delete(_toDelete), _toDelete);
		}

		[TestMethod]
		public void Delete_RemovesTheEntityWithId_ReturnsTheEntityDeleted()
		{
			// Arrange
			object _toDelete = 2;
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);

			// Assert
			Assert.AreEqual(_repository.Delete(_toDelete), 2);
		}

		[TestMethod]
		public void Delete_ThrowsInvalidOperationException_IfEntityNotFound()
		{
			// Arrange
			object _toDelete = 4;
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			void Test()
			{
				_repository.Delete(_toDelete);
			}

			// Assert
			Assert.ThrowsException<InvalidOperationException>(Test);
		}

		[TestMethod]
		public void Find_ThrowsArgumentNullException_IfPredicateIsNull()
		{
			// Arrange
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			void Test()
			{
				_repository.Find(null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void FindAll_ThrowsArgumentNullException_IfPredicateIsNull()
		{
			// Arrange
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			void Test()
			{
				_repository.FindAll(null);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void FindAll_ReturnsACollectionOfAllElementsByPredeciate_IfPredicateIsGiven()
		{
			// Arrange
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			IList<int> _result = new List<int>(_repository.FindAll(curr => curr < 3));

			// Assert
			Assert.AreEqual(2, _result.Count);
			Assert.AreEqual(1, _result[0]);
			Assert.AreEqual(2, _result[1]);
		}

		[TestMethod]
		public void GetAll_ReturnsAllElements_IfSet()
		{
			// Arrange
			ICollection<int> _collection = new List<int>()
			{
				1,
				2,
				3
			};
			CollectionRepository<int> _repository;

			// Act
			_repository = new CollectionRepository<int>(_collection);
			ICollection<int> _result = _repository.GetAll();

			// Assert
			Assert.AreEqual(_collection, _result);
		}
	}
}
