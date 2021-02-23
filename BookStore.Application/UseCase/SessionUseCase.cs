using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IHasherService hasher;
        private readonly ITokenManagerService tokenManaherService;
        private readonly IConfigurations configs;
        public SessionUseCase(IUserRepository userRepository, IHasherService hasher, ITokenManagerService tokenManaherService, IConfigurations configs) 
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
            this.tokenManaherService = tokenManaherService;
            this.configs = configs;
        }
        public async Task<Session> CreateUserSession(UserCredentials credentials)
        {
            var user = await FindUserOrTrhow(credentials);

            PasswordAndPermissionValidate(credentials, user);
            var expireIn = DateTime.UtcNow.AddHours(configs.JwtExpiredHours);
            var tokenData = new TokenData()
            {
                Id = user.Id,
                Guid = user.Guid.ToString(),
                ExpirationDate = expireIn
            };
            return new Session()
            {
                AccessToken = tokenManaherService.CreateToken(tokenData),
                ExpireIn = expireIn
            };
        }

        #region privateMethods
        private async Task<User> FindUserOrTrhow(UserCredentials credentials)
        {
            var user = await userRepository.FindByEmail(credentials.Email);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return user;
        }

        private void PasswordAndPermissionValidate(UserCredentials credentials, User user)
        {
            var result = hasher.CompareHashe(credentials.Password.ToString(), user.PasswordHash);
            if (!result)
            {
                throw new WrongPasswordException();
            }

            if (user.Permission == Permissions.Unauthorized)
            {
                throw new UnauthorizedExcpetion();
            }
        }
        #endregion
    }
}
