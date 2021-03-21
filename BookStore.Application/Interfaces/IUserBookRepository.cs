using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUserBookRepository
    {
        Task<Either<NotificationBase, UserBook>> SaveUserBook(UserBook author);
        Task<Either<NotificationBase, UserBook>> FindById(int id);
        Task<Either<NotificationBase, IEnumerable<UserBook>>> FindAll();
        Task<Either<NotificationBase, bool>> Update(UserBook author);
        Task<Either<NotificationBase, bool>> DeleteById(int id);
    }
}
