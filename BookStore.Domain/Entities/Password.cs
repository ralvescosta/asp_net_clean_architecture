using System;

namespace BookStore.Domain.Entities
{
    public struct Password
    {
        private readonly string password;

        private Password(string password)
        {
            this.password = password;
        }

        public static Password Parse(string value)
        {
            if(TryParse(value, out var result))
            {
                return result;
            }
            throw new ArgumentException("");
        }

        public static bool TryParse(string value, out Password password)
        {
            if(value.Length > 5)
            {
                password = new Password(value);
                return true;
            }
            else 
            {
                password = new Password(value);
                return false;
            }
        }

        public override string ToString() => password;

        public static implicit operator Password(string input) => Parse(input);
    }
}
