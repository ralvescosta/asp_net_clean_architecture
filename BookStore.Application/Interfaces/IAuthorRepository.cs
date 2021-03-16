using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Either<NotificationBase, Author>> SaveAuthor(Author author);
        Task<Either<NotificationBase, Author>> FindByGuid(string guid);
        Task<Either<NotificationBase, Author>> FindById(int id);
        Task<Either<NotificationBase, IEnumerable<Author>>> FindAll();
        Task<Either<NotificationBase, bool>> Update(Author author);
        Task<Either<NotificationBase, bool>> DeleteById(int id);
    }
}
