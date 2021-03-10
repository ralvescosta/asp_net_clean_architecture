using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
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
        public async Task<Either<NotificationBase, Session>> CreateUserSession(SessionRequestDTO credentials)
        {
            var userEither = await userRepository.FindByEmail(credentials.Email.ToString());
            if(userEither.IsLeft())
                return new Left<NotificationBase, Session>(userEither.GetLeft());
            
            if (userEither.GetRight() == null)
                return new Left<NotificationBase, Session>(new NotFoundNotification());

            var user = userEither.GetRight();
            if (user.Permission == Permissions.Unauthorized)
                return new Left<NotificationBase, Session>(new UnauthorizedNotification());

            var result = hasher.CompareHashe(credentials.Password, user.PasswordHash);
            if (!result)
                return new Left<NotificationBase, Session>(new WrongPasswordNotification());

            var expireIn = DateTime.UtcNow.AddHours(configs.JwtExpiredHours);
            var tokenData = new TokenData()
            {
                Id = user.Id,
                Guid = user.Guid.ToString(),
                ExpirationDate = expireIn
            };
            var session = new Session()
            {
                AccessToken = tokenManaherService.CreateToken(tokenData),
                ExpireIn = expireIn
            };

            return new Right<NotificationBase, Session>(session);
        }

        #region privateMethods
        #endregion
    }
}
