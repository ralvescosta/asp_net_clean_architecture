using BookStore.Application.Interfaces;
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
        private readonly IUserBookRepository userBookRepository;
        public BorrowBookUseCase(IUserBookRepository userBookRepository) 
        {
            this.userBookRepository = userBookRepository;
        }
        public Task<Either<NotificationBase, UserBook>> BorrowABook()
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, IEnumerable<UserBook>>> GetAllBorrowedBook()
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, UserBook>> GetAnBorrowedBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, UserBook>> UpdateABorrowedBookById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
