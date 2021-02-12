using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Services
{
    public class Hasher : IHasher
    {
        public string Hashe(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
