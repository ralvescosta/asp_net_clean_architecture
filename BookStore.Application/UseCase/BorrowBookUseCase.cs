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
                ExpiredAt = DateTime.Now.AddDays(maxBorrowedDays),
            };
            var result = await userBookRepository.SaveUserBook(userBook);
            if (result.IsLeft())
                return new Left<NotificationBase, UserBook>(result.GetLeft());

            return new Right<NotificationBase, UserBook>(userBook);
        }

        public async Task<Either<NotificationBase, IEnumerable<UserBook>>> GetAllBorrowedBook()
        {
            var usersBooks = await userBookRepository.FindAll();
            if (usersBooks.IsLeft())
                return new Left<NotificationBase, IEnumerable<UserBook>>(usersBooks.GetLeft());

            return new Right<NotificationBase, IEnumerable<UserBook>>(usersBooks.GetRight());
        }

        public async Task<Either<NotificationBase, UserBook>> GetAnBorrowedBookById(int id)
        {
            var userBook = await userBookRepository.FindById(id);
            if (userBook.IsLeft())
                return new Left<NotificationBase, UserBook>(userBook.GetLeft());

            if (userBook.GetRight() == null)
                return new Left<NotificationBase, UserBook>(new NotFoundNotification("Borrow Not Found"));

            return new Right<NotificationBase, UserBook>(userBook.GetRight());
        }

        public async Task<Either<NotificationBase, UserBook>> UpdateABorrowedBookById(int id)
        {
            var userBook = await userBookRepository.FindById(id);
            if (userBook.IsLeft())
                return new Left<NotificationBase, UserBook>(userBook.GetLeft());

            if (userBook.GetRight() == null)
                return new Left<NotificationBase, UserBook>(new NotFoundNotification("Borrow Not Found"));

            return new Right<NotificationBase, UserBook>(new UserBook { });
        }
        public async Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id)
        {
            var userBook = await userBookRepository.FindById(id);
            if (userBook.IsLeft())
                return new Left<NotificationBase, bool>(userBook.GetLeft());

            if (userBook.GetRight() == null)
                return new Left<NotificationBase, bool>(new NotFoundNotification("Borrow Not Found"));

            var result = await userBookRepository.DeleteById(id);
            if(result.IsLeft())
                return new Left<NotificationBase, bool>(userBook.GetLeft());

            return new Right<NotificationBase, bool>(true);
        }
    }
}
