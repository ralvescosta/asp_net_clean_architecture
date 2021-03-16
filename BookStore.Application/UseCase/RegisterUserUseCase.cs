using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.UseCase
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IHasherService hasher;
        public RegisterUserUseCase(IUserRepository userRepository, IHasherService hasher)
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
        }
        public async Task<Either<NotificationBase, User>> Register(SignUpRequestDTO user)
        {
            var savedUser = await userRepository.FindByEmail(user.Email);
            if (savedUser.IsLeft())
                return new Left<NotificationBase, User>(savedUser.GetLeft());

            if(savedUser.GetRight() != null)
            {
                return new Left<NotificationBase, User>(new AlreadyExistNotification("Email already exist"));
            }

            var newUser = new User
            {
                Guid = Guid.NewGuid().ToString(),
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Permission = Permissions.User,
                PasswordHash = hasher.Hashe(user.Password)
            };
            var result = await userRepository.SaveUser(newUser);
            if(result.IsLeft())
                return new Left<NotificationBase, User>(result.GetLeft());

            return new Right<NotificationBase, User>(result.GetRight());
        }
    }
}
