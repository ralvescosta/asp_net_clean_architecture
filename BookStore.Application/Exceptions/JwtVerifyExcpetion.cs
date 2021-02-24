using System;

namespace BookStore.Application.Exceptions
{
    public class JwtVerifyExcpetion : Exception
    {
        public JwtVerifyExcpetion(string message = "") : base(message)
        {
        }
    }
}
