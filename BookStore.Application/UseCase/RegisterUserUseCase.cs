﻿using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.DTOs;
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
        private readonly IHasherService hasher;
        public RegisterUserUseCase(IUserRepository userRepository, IHasherService hasher)
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
        }
        public async Task<User> Register(UserRegistrationRequestDTO user)
        {
            await CheckIfUserExist(user.Email);
            return await PersistUser(user);
        }

        #region privateMethods
        private async Task CheckIfUserExist(string email) 
        {
            User user;
            try
            {
                user = await userRepository.FindByEmail(email);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            if (user != null)
            {
                throw new EmailAlreadyExistException("Email Already Exist");
            }
        }

        private Task<User> PersistUser(UserRegistrationRequestDTO input)
        {
            try
            {
                var user = new User
                {
                    Guid = Guid.NewGuid().ToString(),
                    Name = input.Name,
                    LastName = input.LastName,
                    Email = input.Email,
                    Permission = Permissions.Admin,
                    PasswordHash = hasher.Hashe(input.Password)
                };
                return userRepository.SaveUser(user);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion
    }
}
