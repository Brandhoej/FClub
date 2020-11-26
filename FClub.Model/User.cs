using FClub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FClub.Model
{
    public class User : IComparable<User>
    {
        public event balanceNotification OnBalanceNotification;

        private string m_firstName;
        private string m_lastName;
        private string m_username;
        private string m_email;
        private decimal m_balance;

        public User(IIdentifier identifier, string firstName, string lastName, string username, string email)
            : this(identifier ?? throw new ArgumentNullException(nameof(identifier), "Identifier cannot be null"),
				   firstName,
				   lastName,
				   username,
				   email,
				   decimal.Zero)
        { }

        public User(IIdentifier identifier, string firstName, string lastName, string username, string email, decimal balance)
            : this(identifier == null ? throw new ArgumentNullException(nameof(identifier), "Identifier cannot be null") : identifier.GetNextId(),
				   firstName,
				   lastName,
				   username,
				   email,
				   balance)
        { }

        public User(int id, string firstName, string lastName, string username, string email, decimal balance)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Balance = balance;
        }

        public delegate void balanceNotification(User user, decimal balance);

        public int Id { get; }

        public string FirstName
        {
            get => m_firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Argument cannot be null or emtpy", nameof(value));
                }

                m_firstName = value;
            }
        }

        public string LastName
        {
            get => m_lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Argument cannot be null or emtpy", nameof(value));
                }

                m_lastName = value;
            }
        }

        public string Username
        {
            get => m_username;
            set
            {
                // TODO: Check specification
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Argument cannot be null or emtpy", nameof(value));
                }

                Regex _usernamePattern = new Regex(@"[a-z0-9_]", RegexOptions.Compiled);

                if (!_usernamePattern.IsMatch(value))
                {
                    throw new ArgumentException($"Username does not follow correct format '{value}''", nameof(value));
                }

                m_username = value;
            }
        }

        public string Email
        {
            get => m_email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Argument cannot be null or emtpy", nameof(value));
                }

                Regex _emailPattern = new Regex(@"(^[\w-,]+)@(([\w]+\.)+[\w]+(?=[\s]|$))", RegexOptions.Compiled);

                if (!_emailPattern.IsMatch(value))
                {
                    throw new ArgumentException($"Email does not follow correct format '{value}''", nameof(value));
                }

                m_email = value;
            }
        }

        public decimal Balance
        {
            get => m_balance;
            set
            {
                if (value < 50)
                {
                    OnBalanceNotification?.Invoke(this, value);
                }

                m_balance = value;
            }
        }

        public int CompareTo(User other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            // Per implementation the Id is unique (User is aggregate root)
            return obj is User user &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }
    }
}
