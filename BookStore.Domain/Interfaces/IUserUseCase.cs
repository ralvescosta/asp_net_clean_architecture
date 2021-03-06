﻿using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IUserUseCase
    {
        Task<Either<NotificationBase, IEnumerable<User>>> GetAllUsers();
        Task<Either<NotificationBase, User>> GetAnUserById(int id);
        Task<Either<NotificationBase, User>> UpdateAnUserById(int id, UpdateUserRequestDTO update);
        Task<Either<NotificationBase, bool>> DeleteAnUserById(int id);
    }
}
