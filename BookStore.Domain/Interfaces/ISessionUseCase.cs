using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ISessionUseCase
    {
        Task<Either<NotificationBase, Session>> CreateUserSession(SessionRequestDTO credentials);
    }
}
