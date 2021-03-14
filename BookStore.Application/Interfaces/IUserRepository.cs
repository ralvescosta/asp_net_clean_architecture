using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Either<NotificationBase, User>> SaveUser(User user);
        Task<Either<NotificationBase, User>> FindByEmail(string email);
        Task<Either<NotificationBase, User>> FindById(int id);
        Task<Either<NotificationBase, IEnumerable<User>>> FindAll();
        Task<Either<NotificationBase, bool>> Update(User user);
        Task<Either<NotificationBase, bool>> DeleteBtId(int id);
    }
}
