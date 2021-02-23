using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface ITokenManagerService
    {
        string CreateToken(TokenData input);
        TokenData VerifyToken(string Token);
    }
}
