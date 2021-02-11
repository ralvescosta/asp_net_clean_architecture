using BookStore.Application.Exceptions;
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
        private readonly IHasher hasher;
        public RegisterUserUseCase(IUserRepository userRepository, IHasher hasher)
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
        }
        public Task<User> Register(UserRegistrationDTO input)
        {
            var user = userRepository.FindByEmail(input.Email);
            if (user != null)
            {
                throw new EmailAlreadyExistException();
            }

            try
            {
                user = new User()
                {
                    Guid = Guid.NewGuid(),
                    Name = input.Name,
                    LastName = input.LastName,
                    Email = input.Email,
                    Permission = Permissions.User,
                    PasswordHash = hasher.Hashe(input.Password.ToString())
                };
                userRepository.CreateUser(user);
                return Task.FromResult(user);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
