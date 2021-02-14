using System;

namespace BookStore.Application.Exceptions
{
    public class UnauthorizedExcpetion : Exception
    {
        public UnauthorizedExcpetion(string message = "") : base(message)
        {
        }
    }
}
