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
    public class AutherUseCase : IAuthorUseCase
    {
        private readonly IAuthorRepository authorRepository;
        public AutherUseCase(IAuthorRepository authorRepository) 
        {
            this.authorRepository = authorRepository;
        }

        public async Task<Either<NotificationBase, Author>> CreateAuthor(CreateAuthorRequestDTO create)
        {
            var author = await authorRepository.FindByName(create.FirstName, create.LastName);
            if (author.IsLeft())
                return new Left<NotificationBase, Author>(author.GetLeft());

            if(author.GetRight() != null)
                return new Left<NotificationBase, Author>(new AlreadyExistNotification());

            author = await authorRepository.SaveAuthor(new Author
            {
                FirstName = create.FirstName,
                LastName = create.LastName,
                Description = create.Description,
                Guid = Guid.NewGuid().ToString()
            });

            return new Right<NotificationBase, Author>(author.GetRight());
        }

        public async Task<Either<NotificationBase, IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await authorRepository.FindAll();
            if (authors.IsLeft())
                return new Left<NotificationBase, IEnumerable<Author>>(authors.GetLeft());

            return new Right<NotificationBase, IEnumerable<Author>>(authors.GetRight());
        }

        public async Task<Either<NotificationBase, Author>> GetAnUserById(int id)
        {
            var author = await authorRepository.FindById(id);
            if (author.IsLeft())
                return new Left<NotificationBase, Author>(author.GetLeft());

            return new Right<NotificationBase, Author>(author.GetRight());
        }

        public async Task<Either<NotificationBase, Author>> UpdateAnUserById(int id, UpdateAuthorRequestDTO update)
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

        public async Task<Either<NotificationBase, bool>> DeleteAnUserById(int id)
        {
            var deleted = await authorRepository.DeleteById(id);
            if (deleted.IsLeft())
                return new Left<NotificationBase, bool>(deleted.GetLeft());

            return new Right<NotificationBase, bool>(true);
        }
    }
}
