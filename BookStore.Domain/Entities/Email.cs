using System;
using System.Text.RegularExpressions;

namespace BookStore.Domain.Entities
{
    public struct Email
    {
        private readonly string email;
        private Email(string email)
        {
            this.email = email;
        }

        public static Email Parse(string value) 
        { 
            if(TryParse(value, out var result))
            {
                return result;
            }
            throw new ArgumentException("Email is wrong");
        }

        public static bool TryParse(string value, out Email email)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(value))
            {
                email = new Email(value);
                return true;
            }
            else
            {
                email = new Email(value);
                return false;
            }            
        }

        public override string ToString() => email;

        public static implicit operator Email(string input) => Parse(input);
    }
}
