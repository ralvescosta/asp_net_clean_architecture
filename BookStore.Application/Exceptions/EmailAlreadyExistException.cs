using System;

namespace BookStore.Application.Exceptions
{
    public class EmailAlreadyExistException : Exception
    {
        public EmailAlreadyExistException(string message) : base(message) { }
    }
}
