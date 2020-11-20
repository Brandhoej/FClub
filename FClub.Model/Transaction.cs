﻿using FClub.Core;
using System;

namespace FClub.Model
{
    public abstract class Transaction
    {
        public Transaction(IIdentifier identifier, User user, decimal amount)
            : this(identifier ?? throw new ArgumentNullException("Identifier cannot be null", nameof(identifier)), 
                  user, amount, DateTime.Now)
        { }

        public Transaction(IIdentifier identifier, User user, decimal amount, DateTime date)
            : this(identifier == null ? throw new ArgumentNullException("Identifier cannot be null", nameof(identifier)) : identifier.GetNextId(), 
                   user ?? throw new ArgumentNullException("User cannot be null", nameof(user)), amount, date)
        { }

        protected Transaction(int id, User user, decimal amount, DateTime date)
        {
            Id = id;
            User = user;
            Amount = amount;
            Date = date;
        }

        public int Id { get; }
        public User User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }

        public virtual void Execute()
        {
            if (User == null)
            {
                throw new NullReferenceException("Transaction user cannot be null");
            }

            User.Balance += Amount;
        }

        public override string ToString()
        {
            return $"{Id}: {User.Username} {Amount} {Date}";
        }
    }
}