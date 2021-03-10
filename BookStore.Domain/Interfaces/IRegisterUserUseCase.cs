using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IRegisterUserUseCase
    {
        Task<Either<NotificationBase, User>> Register(UserRegistrationRequestDTO user);
    }
}
