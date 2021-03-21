using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBorrowBookUseCase
    {
        Task<Either<NotificationBase, UserBook>> BorrowABook(CreateBorrowBookRequestDTO borrowBook);
        Task<Either<NotificationBase, IEnumerable<UserBook>>> GetAllBorrowedBook();
        Task<Either<NotificationBase, UserBook>> GetAnBorrowedBookById(int id);
        Task<Either<NotificationBase, UserBook>> UpdateABorrowedBookById(int id);
        Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id);
    }
}
