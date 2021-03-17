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

        public Task<Either<NotificationBase, bool>> DeleteAnBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, IEnumerable<Author>>> GetAllBooks()
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, Author>> GetAnBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Either<NotificationBase, Author>> UpdateAnBookById(int id, object update)
        {
            throw new System.NotImplementedException();
        }
    }
}
