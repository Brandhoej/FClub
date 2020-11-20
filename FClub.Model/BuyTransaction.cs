using FClub.Core;
using System;

namespace FClub.Model
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(IIdentifier identifier, User user, Product product)
            : this(identifier, user, product ?? throw new ArgumentNullException("Product cannot be null", nameof(product)), DateTime.Now)
        { }

        public BuyTransaction(IIdentifier identifier, User user, Product product, DateTime date)
            : this(identifier, user, product == null ? throw new ArgumentNullException("Product cannot be null", nameof(product)) : product.Price, date)
        {
            Product = product;
        }

        protected BuyTransaction(IIdentifier identifier, User user, decimal price, DateTime date)
            : base(identifier, user, -price, date)
        { }

        public Product Product { get; }

        public override void Execute()
        {
            if (User.Balance + Amount <= 0)
            {
                throw new InsufficientCreditsException(User, Product);
            }

            base.Execute();
        }
    }
}
