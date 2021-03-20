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
        Task<Either<NotificationBase, UsersBooks>> BorrowABook();
        Task<Either<NotificationBase, IEnumerable<UsersBooks>>> GetAllBorrowedBook();
        Task<Either<NotificationBase, UsersBooks>> GetAnBorrowedBookById(int id);
        Task<Either<NotificationBase, UsersBooks>> UpdateABorrowedBookById(int id);
        Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id);
    }
}
