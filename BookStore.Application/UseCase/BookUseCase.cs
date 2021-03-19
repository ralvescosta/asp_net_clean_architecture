using BookStore.Application.Interfaces;
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
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookRepository bookRepository;
        public BookUseCase(IBookRepository bookRepository) 
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Either<NotificationBase, Book>> CreateBook(CreateBookRequestDTO create)
        {
            var book = new Book { 
                AuthorId = create.AuthorId, 
                Title = create.Title, 
                Subtitle = create.Subtitle, 
                Subject = create.Subject, 
                Guid = Guid.NewGuid().ToString() 
            };
            var result = await bookRepository.Create(book);
            if (result.IsLeft())
                return new Left<NotificationBase, Book>(result.GetLeft());

            return new Right<NotificationBase, Book>(result.GetRight());
        }

        public async Task<Either<NotificationBase, IEnumerable<Book>>> GetAllBooks()
        {
            var books = await bookRepository.FindAll();
            if (books.IsLeft())
                return new Left<NotificationBase, IEnumerable<Book>>(books.GetLeft());

            return new Right<NotificationBase, IEnumerable<Book>>(books.GetRight());
        }

        public async Task<Either<NotificationBase, Book>> GetAnBookById(int id)
        {
            var book = await bookRepository.FindById(id);
            if (book.IsLeft())
                return new Left<NotificationBase, Book>(book.GetLeft());

            return new Right<NotificationBase, Book>(book.GetRight());
        }

        public async Task<Either<NotificationBase, Book>> UpdateAnBookById(int id, UpdateBookRequestDTO update)
        {
            var book = new Book { Id = id, Title = update.Title, Subtitle = update.Subtitle, Subject = update.Subject };
            var result = await bookRepository.Update(book);
            if (result.IsLeft())
                return new Left<NotificationBase, Book>(result.GetLeft());

            return new Right<NotificationBase, Book>(book);
        }

        public async Task<Either<NotificationBase, bool>> DeleteAnBookById(int id)
        {
            var result = await bookRepository.DeleteById(id);
            if (result.IsLeft())
                return new Left<NotificationBase, bool>(result.GetLeft());

            return new Right<NotificationBase, bool>(result.GetRight());
        }
    }
}
