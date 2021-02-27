using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface ITokenManagerService
    {
        string CreateToken(TokenData input);
        TokenData VerifyToken(string Token);
    }
}
