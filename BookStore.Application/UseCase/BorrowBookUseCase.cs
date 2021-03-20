using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        public Task<Either<NotificationBase, UsersBooks>> BorrowABook()
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, IEnumerable<UsersBooks>>> GetAllBorrowedBook()
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, UsersBooks>> GetAnBorrowedBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, UsersBooks>> UpdateABorrowedBookById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
