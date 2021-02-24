using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
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
        public async Task<AuthenticatedUser> Auth(string authorizationHeader, Permissions permissionRequired)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new ApplicationException();
            }
            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                throw new ApplicationException();
            }

            string token = authorizationHeader["Bearer".Length..].Trim();
            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException();
            }

            var user = await userRepository.FindById(1);
            if (user == null) return null;

            switch (permissionRequired)
            {
                case Permissions.Admin:
                    if (user.Permission != Permissions.Admin)
                    {
                        return null;
                    }
                    break;
                case Permissions.User:
                    if (user.Permission != Permissions.Admin || user.Permission != Permissions.User)
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
    }
}
