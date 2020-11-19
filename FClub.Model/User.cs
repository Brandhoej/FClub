using System;
using System.Collections.Generic;

namespace FClub.Model
{
    public class User : IComparable<User>
    {
        public event userBalanceNotification OnUserBalanceNotification;

        private static int m_counter = 0;

        private string m_firstName;
        private string m_lastName;
        private string m_username;
        private string m_email;
        private decimal m_balance;

        public User(string firstName, string lastName, string username, string email)
            : this(firstName, lastName, username, email, decimal.Zero)
		{ }

        public User(string firstName, string lastName, string username, string email, decimal balance)
        {
            Id = m_counter;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Balance = balance;

            m_counter++;
        }

        public delegate void userBalanceNotification(User user, decimal balance);

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

                m_username = value;
            }
        }

        public string Email
        {
            get => m_email;
            set
            {
                // TODO: Check specification
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Argument cannot be null or emtpy", nameof(value));
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
                    OnUserBalanceNotification?.Invoke(this, value);
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
            /*int hashCode = 140654095;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + Balance.GetHashCode();
            return hashCode;*/
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }
    }
}
