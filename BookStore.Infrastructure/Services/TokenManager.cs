using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Services
{
    public class TokenManager :  ITokenManager
    {
        public string CreateToken(TokenData input)
        {
            return $"Id: {input.Id}, Guid: ${input.Guid}";
        }
    }
}
