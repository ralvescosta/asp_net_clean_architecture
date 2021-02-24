using BookStore.Application.Exceptions;
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
                throw new UnauthorizedExcpetion();
            }
            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedExcpetion();
            }

            string token = authorizationHeader["Bearer".Length..].Trim();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedExcpetion();
            }

            TokenData tokenData;
            try
            {
                tokenData = tokenManagerService.VerifyToken(token);
            }
            catch
            {
                throw new UnauthorizedExcpetion();
            }
            
            var user = await userRepository.FindById(tokenData.Id);
            if (user == null) throw new UnauthorizedExcpetion();

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
