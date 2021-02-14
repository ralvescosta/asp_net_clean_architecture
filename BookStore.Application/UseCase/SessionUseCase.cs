using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;
        private readonly ITokenManager tokenManaher;
        public SessionUseCase(IUserRepository userRepository, IHasher hasher, ITokenManager tokenManaher) 
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
            this.tokenManaher = tokenManaher;
        }
        public async Task<Session> CreateUserSession(UserCredentials credentials)
        {
            var user = await FindUserOrTrhow(credentials);

            PasswordAndPermissionValidate(credentials, user);

            var tokenData = new TokenData()
            {
                Id = user.Id,
                Guid = user.Guid.ToString(),
            };
            return new Session()
            {
                AccessToken = tokenManaher.CreateToken(tokenData)
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
