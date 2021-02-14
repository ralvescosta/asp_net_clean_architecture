﻿using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;
        public RegisterUserUseCase(IUserRepository userRepository, IHasher hasher)
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
        }
        public async Task<User> Register(UserRegistration input)
        {
            await CheckIfUserExist(input.Email);
            return await PersistUser(input);
        }

        #region privateMethods
        private async Task CheckIfUserExist(Email email) 
        {
            User user;
            try
            {
                user = await userRepository.FindByEmail(email);
            }
            catch
            {
                throw new ApplicationException();
            }
            if (user != null)
            {
                throw new EmailAlreadyExistException("Email Already Exist");
            }
        }

        private Task<User> PersistUser(UserRegistration input)
        {
            try
            {
                User user = new User()
                {
                    Guid = Guid.NewGuid(),
                    Name = input.Name,
                    LastName = input.LastName,
                    Email = input.Email,
                    Permission = Permissions.User,
                    PasswordHash = hasher.Hashe(input.Password.ToString())
                };
                return userRepository.SaveUser(user);
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}
