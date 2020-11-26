using FClub.DAL.IO;
using FClub.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.DAL.Tests
{
	[TestClass]
	public class UsersReaderTest
	{
		[TestMethod]
		public void UsersReader_SuccessfullyConstructs_IfCorrectParameters()
		{
			// Arrange
			string _separator = "separator";
			string _path = "path";
			UsersReader _usersReader;

			// Act
			_usersReader = new UsersReader(_path, _separator);

			// Assert
			Assert.AreEqual(_separator, _usersReader.Separator);
			Assert.AreEqual(_path, _usersReader.Path);
		}

		[TestMethod]
		public void UsersReader_ThrowsArgumentNullException_IfPathIsNUll()
		{
			// Arrange
			string _separator = "separator";
			string _path = null;
			UsersReader _usersReader;

			// Act
			void Test()
			{
				_usersReader = new UsersReader(_separator, _path);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void UsersReader_ThrowsArgumentNullException_IfSeparatorIsNUll()
		{
			// Arrange
			string _separator = null;
			string _path = "path";
			UsersReader _usersReader;

			// Act
			void Test()
			{
				_usersReader = new UsersReader(_separator, _path);
			}

			// Assert
			Assert.ThrowsException<ArgumentNullException>(Test);
		}

		[TestMethod]
		public void CreateUserFromLine_ThrowsArgumentException_IfInputHasLessThan6Separators()
		{
			// Arrange
			string _separator = ",";
			string _path = "path";
			UsersReader _usersReader;

			// Act
			void Test()
			{
				_usersReader = new UsersReader(_separator, _path);
				_usersReader.Construct(string.Empty);
			}

			// Assert
			Assert.ThrowsException<ArgumentException>(Test);
		}

		[TestMethod]
		public void CreateUserFromLine_ConstructsUserSuccessfully_IfCorrectInput()
		{
			// Arrange
			int _id = 1;
			string _firstName = "Andreas";
			string _lastName = "Brandhoej";
			string _username = "Hyw";
			decimal _balance = 123;
			string _email = "akbr18@student.aau.dk";
			string _separator = ",";
			string _path = "path";
			UsersReader _usersReader;
			User _user;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_firstName);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_lastName);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_username);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_balance);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_email);
			_stringBuilder.Append(_separator);

			_usersReader = new UsersReader(_path, _separator);
			_user = _usersReader.Construct(_stringBuilder.ToString());

			// Assert
			Assert.AreEqual(_separator, _usersReader.Separator);
			Assert.AreEqual(_path, _usersReader.Path);
			Assert.AreEqual(_id, _user.Id);
			Assert.AreEqual(_firstName, _user.FirstName);
			Assert.AreEqual(_lastName, _user.LastName);
			Assert.AreEqual(_username, _user.Username);
			Assert.AreEqual(_balance, _user.Balance);
			Assert.AreEqual(_email, _user.Email);
		}

		[TestMethod]
		public void CreateUserFromLine_ReturnsDefault_IfUserRulesAreNotFollowed()
		{
			// Arrange
			int _id = 1;
			string _firstName = "Andreas";
			string _lastName = "Brandhoej";
			string _username = "Hyw";
			decimal _balance = 123;
			string _email = "akbr18";
			string _separator = ",";
			string _path = "path";
			UsersReader _usersReader;
			User _user;
			StringBuilder _stringBuilder;

			// Act
			_stringBuilder = new StringBuilder();
			_stringBuilder.Append(_id);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_firstName);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_lastName);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_username);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_balance);
			_stringBuilder.Append(_separator);
			_stringBuilder.Append(_email);
			_stringBuilder.Append(_separator);

			_usersReader = new UsersReader(_path, _separator);
			_user = _usersReader.Construct(_stringBuilder.ToString());

			// Assert
			Assert.AreEqual(_separator, _usersReader.Separator);
			Assert.AreEqual(_path, _usersReader.Path);
			Assert.AreEqual(default, _user);
		}
	}
}
