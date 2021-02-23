using BookStore.Application.Interfaces;

namespace BookStore.Infrastructure.Services
{
    public class HasherService : IHasherService
    {
        public bool CompareHashe(string real, string digest)
        {
            return BCrypt.Net.BCrypt.Verify(real, digest);
        }

        public string Hashe(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
