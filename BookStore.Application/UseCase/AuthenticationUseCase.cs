using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class AuthenticationUseCase : IAuthenticationUseCase
    {
        public Task<AuthenticatedUser> Auth(string authorizationHeader)
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

            return Task.FromResult(new AuthenticatedUser());
        }
    }
}
