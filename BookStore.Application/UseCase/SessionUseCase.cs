using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;
        public SessionUseCase(IUserRepository userRepository, IHasher hasher) 
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
        }
        public async Task<Session> CreateUserSession(UserCredentials credentials)
        {
            var user = await userRepository.FindByEmail(credentials.Email);
            if(user == null)
            {
                throw new NotFoundException();
            }

            var result = hasher.CompareHashe(credentials.Email.ToString(), user.PasswordHash);
            if (!result)
            {
                throw new WrongPasswordException();
            }

            return new Session() 
            { 
                AccessToken = "access_token"
            };
        }
    }
}
