using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface ITokenManagerService
    {
        string CreateToken(TokenData input);
        Either<NotificationBase, TokenData> VerifyToken(string Token);
    }
}
