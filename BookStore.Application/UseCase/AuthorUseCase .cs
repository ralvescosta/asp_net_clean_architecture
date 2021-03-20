using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;
using BookStore.Shared.Utils;
using BookStore.Shared.Notifications;
using BookStore.Domain.DTOs;
using BookStore.Application.Notifications;
using System;

namespace BookStore.Application.UseCase
{
    public class AuthorUseCase : IAuthorUseCase
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorUseCase(IAuthorRepository authorRepository) 
        {
            this.authorRepository = authorRepository;
        }

        public async Task<Either<NotificationBase, Author>> CreateAuthor(CreateAuthorRequestDTO create)
        {
            var search = await authorRepository.FindByName(create.FirstName, create.LastName);
            if (search.IsLeft())
                return new Left<NotificationBase, Author>(search.GetLeft());

            if(search.GetRight() != null)
                return new Left<NotificationBase, Author>(new AlreadyExistNotification());

            var author = new Author
            {
                FirstName = create.FirstName,
                LastName = create.LastName,
                Description = create.Description,
                Guid = Guid.NewGuid().ToString()
            };
            var result = await authorRepository.SaveAuthor(author);
            if (result.IsLeft())
                return new Left<NotificationBase, Author>(result.GetLeft());

            return new Right<NotificationBase, Author>(author);
        }

        public async Task<Either<NotificationBase, IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await authorRepository.FindAll();
            if (authors.IsLeft())
                return new Left<NotificationBase, IEnumerable<Author>>(authors.GetLeft());

            return new Right<NotificationBase, IEnumerable<Author>>(authors.GetRight());
        }

        public async Task<Either<NotificationBase, Author>> GetAnAuthorById(int id)
        {
            var author = await authorRepository.FindById(id);
            if (author.IsLeft())
                return new Left<NotificationBase, Author>(author.GetLeft());

            if(author.GetRight() == null)
                return new Left<NotificationBase, Author>(new NotFoundNotification("Author Not Found"));

            return new Right<NotificationBase, Author>(author.GetRight());
        }

        public async Task<Either<NotificationBase, Author>> UpdateAnAuthorById(int id, UpdateAuthorRequestDTO update)
        {
            var author = new Author
            {
                Id = id,
                FirstName = update.FirstName,
                LastName = update.LastName,
                Description = update.Description
            };

            var updated = await authorRepository.Update(author);
            if (updated.IsLeft())
                return new Left<NotificationBase, Author>(updated.GetLeft());

            return new Right<NotificationBase, Author>(author);
        }

        public async Task<Either<NotificationBase, bool>> DeleteAnAuthorById(int id)
        {
            var deleted = await authorRepository.DeleteById(id);
            if (deleted.IsLeft())
                return new Left<NotificationBase, bool>(deleted.GetLeft());

            return new Right<NotificationBase, bool>(true);
        }
    }
}
