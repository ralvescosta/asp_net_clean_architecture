using System;

namespace BookStore.Infrastructure.Exceptions
{
    public class JwtVerifyExcpetion : Exception
    {
        public JwtVerifyExcpetion(string message = "") : base(message)
        {
        }
    }
}
