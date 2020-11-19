using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FClub.Model.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        [DataRow("Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk")]
        [DataRow("Dat Tommy Than", "Dieu", "Nini", "tommy@gun.dk")]
        public void User_CorrectConstructionOfUser_Constructed(string firstName, string lastName, string username, string email)
        {
            // Arrange
            User user;

            // Act
            user = new User(firstName, lastName, username, email);

            // Assert
            Assert.AreEqual(user.FirstName, firstName);
            Assert.AreEqual(user.LastName, lastName);
            Assert.AreEqual(user.Username, username);
            Assert.AreEqual(user.Email, email);
            Assert.AreEqual(user.Balance, 0);
        }

        [TestMethod]
        [DataRow("Andreas", "Brandhoej", "Hyw", "akbr18@student.aau.dk", "123")]
        [DataRow("Dat Tommy Than", "Dieu", "Nini", "tommy@gun.dk", "-321")]
        public void User_CorrectConstructionOfWithStartBalanceUser_Constructed(string firstName, string lastName, string username, string email, string balance)
        {
            // Arrange
            decimal _balance;
            User user;

            // Act
            // Passing a decimal is not a constant expression so it will be passed as string which allows us to use DataRow attributes
            _balance = decimal.Parse(balance);
            user = new User(firstName, lastName, username, email, _balance);

            // Assert
            Assert.AreEqual(user.FirstName, firstName);
            Assert.AreEqual(user.LastName, lastName);
            Assert.AreEqual(user.Username, username);
            Assert.AreEqual(user.Email, email);
            Assert.AreEqual(user.Balance, _balance);
        }

        [TestMethod]
        public void User_UniqueIdsWhichAreInOrderOfCreation_AllUsers()
        {
            // Arrange
            const int _amount = 100;
            IList<User> _users = new List<User>();

            // Act
            while (_users.Count < _amount)
            {
                _users.Add(new User("firstName", "lastName", "username", "email"));
            }

            // Assert
            for (int i = 1; i < _users.Count; i++)
            {
                // Since they all must be less than then we are both checking uniqueness and order
                Assert.AreEqual(_users[i - 1].Id < _users[i].Id, true);
            }
        }

        [TestMethod]
        public void FirstNameSet_ThrowsException_IfValueIsEmpty()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User(string.Empty, "lastName", "username", "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void FirstNameSet_ThrowsException_IfValueIsNull()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User(null, "lastName", "username", "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void LastNameSet_ThrowsException_IfValueIsEmpty()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User("FirstName", string.Empty, "username", "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void LastNameSet_ThrowsException_IfValueIsNull()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User("FirstName", null, "username", "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void UsernameSet_ThrowsException_IfValueIsEmpty()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User("FirstName", "LastName", string.Empty, "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void UsernameSet_ThrowsException_IfValueIsNull()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User("FirstName", "LastName", null, "email");
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void EmailSet_ThrowsException_IfValueIsEmpty()
        {
            // Arrange
            User user;

            // Act
            void Test()
            {
                user = new User("FirstName", "LastName", "Username", string.Empty);
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void EmailSet_ThrowsException_IfValueIsNull()
        {
            // Arrange
            User _user;

            // Act
            void Test()
            {
                _user = new User("FirstName", "LastName", "Username", null);
            }

            // Assert
            Assert.ThrowsException<ArgumentException>(Test);
        }

        [TestMethod]
        public void BalanceSet_ThrowsException_IfValueIsLessThan50()
        {
            // Arrange
            const decimal _balance = 49M;
            bool _onUserBalanceNotificationInvoked = false;
            User _user;

            // Act
            _user = new User("FirstName", "LastName", "Username", "Email");
            _user.OnUserBalanceNotification += (u, b) => _onUserBalanceNotificationInvoked = true;
            _user.Balance = _balance;

            // Assert
            Assert.AreEqual(_onUserBalanceNotificationInvoked, true);
        }

        [TestMethod]
        public void BalanceSet_Successs_IfValueGreaterThanOrEqualTo50()
        {
            // Arrange
            const decimal _balance = 50M;
            bool _onUserBalanceNotificationInvoked = false;
            User _user;

            // Act
            _user = new User("FirstName", "LastName", "Username", "Email");
            _user.OnUserBalanceNotification += (u, b) => _onUserBalanceNotificationInvoked = true;
            _user.Balance = _balance;

            // Assert
            Assert.AreEqual(_onUserBalanceNotificationInvoked, false);
        }

        [TestMethod]
        public void OnUserBalanceNotification_SendsUserAndAttemptedBalance_IfValueIsLessThan50()
        {
            // Arrange
            const decimal _balance = 49M;
            bool _onUserBalanceNotificationInvokedCorrectly = false;
            User _user;

            // Act
            _user = new User("FirstName", "LastName", "Username", "Email");
            _user.OnUserBalanceNotification += (u, b) => _onUserBalanceNotificationInvokedCorrectly = u == _user && b == _balance;
            _user.Balance = _balance;

            // Assert
            Assert.AreEqual(_onUserBalanceNotificationInvokedCorrectly, true);
        }

        [TestMethod]
        public void CompareTo_ReturnsNegative_IfRelativeIdIsLess()
        {
            // Arrange
            User _userA, _userB;

            // Act
            _userA = new User("FirstName", "LastName", "Username", "Email");
            _userB = new User("FirstName", "LastName", "Username", "Email");

            // Assert
            Assert.AreEqual(_userA.CompareTo(_userB) < 0, true);
        }

        [TestMethod]
        public void CompareTo_ReturnsZero_IfRelativeIdsAreEqual()
        {
            // Arrange
            User _userA;

            // Act
            _userA = new User("FirstName", "LastName", "Username", "Email");

            // Assert
            Assert.AreEqual(_userA.CompareTo(_userA) == 0, true);
        }

        [TestMethod]
        public void CompareTo_ReturnsPositive_IfRelativeIdIsGreater()
        {
            // Arrange
            User _userA, _userB;

            // Act
            _userA = new User("FirstName", "LastName", "Username", "Email");
            _userB = new User("FirstName", "LastName", "Username", "Email");

            // Assert
            Assert.AreEqual(_userB.CompareTo(_userA) > 0, true);
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOtherNotUserType()
        {
            // Arrange
            bool _equals = false;
            User _user = new User("FirstName", "LastName", "Username", "Email");

            // Act
            _equals = _user.Equals("String Type");

            // Assert
            Assert.AreEqual(_equals, false);
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfOtherDoesNotHaveTheSameId()
        {
            // Arrange
            bool _equals = false;
            User _userA = new User("FirstName", "LastName", "Username", "Email");
            User _userB = new User("FirstName", "LastName", "Username", "Email");

            // Act
            _equals = _userA.Equals(_userB);

            // Assert
            Assert.AreEqual(_equals, false);
        }

        [TestMethod]
        public void Equals_ReturnsTrue_IfOtherHasTheCorrectTypeAndSameId()
        {
            // Arrange
            bool _equals = false;
            User _user = new User("FirstName", "LastName", "Username", "Email");

            // Act
            _equals = _user.Equals(_user);

            // Assert
            Assert.AreEqual(_equals, true);
        }

        [TestMethod]
        public void GetHashCode_ReturnsIntEqualToTheId_True()
		{
            // Arrange
            bool _equals = false;
            User _user = new User("FirstName", "LastName", "Username", "Email");

            // Act
            _equals = _user.Id == _user.GetHashCode();

            // Assert
            Assert.AreEqual(_equals, true);
        }

        [TestMethod]
        public void ToString_DoesNotThrowException_True()
		{
            // Arrange
            User _user = new User("FirstName", "LastName", "Username", "Email");
			string _toString = string.Empty;

			// Act
			try
			{
                _toString = _user.ToString();
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
