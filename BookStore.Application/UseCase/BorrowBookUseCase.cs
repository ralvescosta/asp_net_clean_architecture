using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        private readonly IUserBookRepository userBookRepository;
        private readonly int maxBorrowedDays = 7;
        public BorrowBookUseCase(IUserBookRepository userBookRepository) 
        {
            this.userBookRepository = userBookRepository;
        }
        public async Task<Either<NotificationBase, UserBook>> BorrowABook(CreateBorrowBookRequestDTO borrowBook)
        {
            var borrowed = await userBookRepository.FindByBookId(borrowBook.BookId);
            if (borrowed.IsLeft())
                return new Left<NotificationBase, UserBook>(borrowed.GetLeft());

            if (borrowed.GetRight() != null)
                return new Left<NotificationBase, UserBook>(new NotAcceptableNotification("Book already borrowed!"));

            var userBook = new UserBook
            {
                Guid = Guid.NewGuid().ToString(),
                UserId = borrowBook.UserId,
                BookId = borrowBook.BookId,
                EpiredAt = DateTime.Now.AddDays(maxBorrowedDays),
            };
            var result = await userBookRepository.SaveUserBook(userBook);
            if (result.IsLeft())
                return new Left<NotificationBase, UserBook>(result.GetLeft());

            return new Right<NotificationBase, UserBook>(userBook);
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
