using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class AuthenticationUseCase : IAuthenticationUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenManagerService tokenManagerService;
        public AuthenticationUseCase(ITokenManagerService tokenManagerService, IUserRepository userRepository) 
        {
            this.tokenManagerService = tokenManagerService;
            this.userRepository = userRepository;
        }
        public async Task<Either<NotificationBase, AuthenticatedUser>> Auth(string authorizationHeader, Permissions permissionRequired)
        {
            var token = GetTokenFromAuthorizationHeader(authorizationHeader);
            if (token == null)
                return new Left<NotificationBase, AuthenticatedUser>(new UnauthorizedNotification());


            var tokenData = tokenManagerService.VerifyToken(token);
            if(tokenData.IsLeft())
                return new Left<NotificationBase, AuthenticatedUser>(new UnauthorizedNotification());

            var user = await userRepository.FindById(tokenData.GetRight().Id);
            if (user.GetRight() == null) 
                return new Left<NotificationBase, AuthenticatedUser>(new UnauthorizedNotification());

            var autheticatedUser = VerifyPermission(user.GetRight(), permissionRequired);
            if(autheticatedUser == null)
                return new Left<NotificationBase, AuthenticatedUser>(new UnauthorizedNotification());

            return new Right<NotificationBase, AuthenticatedUser>(autheticatedUser);
        }

        #region privateMethods
        private static string GetTokenFromAuthorizationHeader(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
                return null;

            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                return null;

            var token = authorizationHeader["Bearer".Length..].Trim();
            if (string.IsNullOrEmpty(token))
                return null;

            return token;
        }
        
        private static AuthenticatedUser VerifyPermission(User user, Permissions permissionRequired)
        {
            switch (permissionRequired)
            {
                case Permissions.Admin:
                    if (user.Permission != Permissions.Admin)
                    {
                        return null;
                    }
                    break;
                case Permissions.User:
                    if (user.Permission != Permissions.Admin && user.Permission != Permissions.User)
                    {
                        return null;
                    }
                    break;
                default:
                    break;
            }

            return new AuthenticatedUser()
            {
                Id = user.Id,
                Guid = user.Guid.ToString(),
                Email = user.Email
            };
        }
        #endregion
    }
}
