using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthenticationUseCase
    {
        Task<Either<NotificationBase, AuthenticatedUser>> Auth(string token, Permissions permissionRequired);
    }
}
