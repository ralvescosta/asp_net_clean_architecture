using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface ITokenManager
    {
        string CreateToken(TokenData input);
        TokenData VerifyToken(string Token);
    }
}
