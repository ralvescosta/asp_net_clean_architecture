using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<Either<NotificationBase, Book>> Create(Book book);
        Task<Either<NotificationBase, Book>> FindByTitle(string title);
        Task<Either<NotificationBase, Book>> FindById(int id);
        Task<Either<NotificationBase, IEnumerable<Book>>> FindAll();
        Task<Either<NotificationBase, bool>> Update(Book book);
        Task<Either<NotificationBase, bool>> DeleteById(int id);
    }
}
