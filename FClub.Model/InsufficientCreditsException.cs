using System;
using System.Runtime.Serialization;

namespace FClub.Model
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User user, Product product)
            : base($"Insufficient credit for {user} to buy {product}")
        { }

        public InsufficientCreditsException(User user, Product product, string message)
            : base($"Insufficient credit for {user} to buy {product} - {message}")
        { }

        public InsufficientCreditsException(User user, Product product, string message, Exception innerException)
            : base($"Insufficient credit for {user} to buy {product} - {message}", innerException)
        { }

        protected InsufficientCreditsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
