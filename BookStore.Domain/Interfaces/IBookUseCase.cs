using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookUseCase
    {
        Task<Either<NotificationBase, Book>> CreateBook(CreateBookRequestDTO create);
        Task<Either<NotificationBase, IEnumerable<Book>>> GetAllBooks();
        Task<Either<NotificationBase, Book>> GetAnBookById(int id);
        Task<Either<NotificationBase, Book>> UpdateAnBookById(int id, object update);
        Task<Either<NotificationBase, bool>> DeleteAnBookById(int id);
    }
}
