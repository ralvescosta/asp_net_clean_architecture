using BookStore.Application.Interfaces;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookRepository bookRepository;
        public BookUseCase(IBookRepository bookRepository) 
        {
            this.bookRepository = bookRepository;
        }

        public Task<Either<NotificationBase, Book>> CreateBook(CreateBookRequestDTO create)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Either<NotificationBase, IEnumerable<Book>>> GetAllBooks()
        {
            var books = await bookRepository.FindAll();
            if (books.IsLeft())
                return new Left<NotificationBase, IEnumerable<Book>>(books.GetLeft());

            return new Right<NotificationBase, IEnumerable<Book>>(books.GetRight());
        }

        public Task<Either<NotificationBase, Book>> GetAnBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, Book>> UpdateAnBookById(int id, object update)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, bool>> DeleteAnBookById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
