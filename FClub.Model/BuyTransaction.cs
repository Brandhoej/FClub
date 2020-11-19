using System;

namespace FClub.Model
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, Product product)
            : base(user, -product.Price)
        {
            Product = product ?? throw new ArgumentNullException("Product cannot be null", nameof(product));
        }

        public Product Product { get; }

        public override void Execute()
        {
            if (User.Balance + Amount >= 0)
            {
                throw new InsufficientCreditsException(User, Product);
            }

            base.Execute();
        }
    }
}
