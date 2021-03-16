﻿using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthorUseCase
    {
        Task<Either<NotificationBase, Author>> CreateAuthor(CreateAuthorRequestDTO create);
        Task<Either<NotificationBase, IEnumerable<Author>>> GetAllAuthors();
        Task<Either<NotificationBase, Author>> GetAnUserById(int id);
        Task<Either<NotificationBase, Author>> UpdateAnUserById(int id, UpdateAuthorRequestDTO update);
        Task<Either<NotificationBase, bool>> DeleteAnUserById(int id);
    }
}
