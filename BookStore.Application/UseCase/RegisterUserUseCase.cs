using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
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
        public Task<User> Register(UserRegistrationDTO user)
        {
            throw new DuplicatedException();
        }
    }
}
