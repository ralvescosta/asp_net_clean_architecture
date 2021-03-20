using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;
using BookStore.Shared.Utils;
using BookStore.Shared.Notifications;
using BookStore.Domain.DTOs;
using BookStore.Application.Notifications;

namespace BookStore.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository userRepository;
        public UserUseCase(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }
        
        public async Task<Either<NotificationBase, IEnumerable<User>>> GetAllUsers()
        {
            var users = await userRepository.FindAll();
            if (users.IsLeft())
                return new Left<NotificationBase, IEnumerable<User>>(users.GetLeft());

            return new Right<NotificationBase, IEnumerable<User>>(users.GetRight());
        }

        public async Task<Either<NotificationBase, User>> GetAnUserById(int id)
        {
            var user = await userRepository.FindById(id);
            if (user.IsLeft())
                return new Left<NotificationBase, User>(user.GetLeft());

            if(user.GetRight() == null)
                return new Left<NotificationBase, User>(new NotFoundNotification("User Not Found"));

            return new Right<NotificationBase, User>(user.GetRight());
        }

        public async Task<Either<NotificationBase, User>> UpdateAnUserById(int id, UpdateUserRequestDTO update)
        {
            var user = new User
            {
                Id = id,
                Name = update.Name,
                LastName = update.LastName,
                Email = update.Email
            };

            var updated = await userRepository.Update(user);
            if (updated.IsLeft())
                return new Left<NotificationBase, User>(updated.GetLeft());

            return new Right<NotificationBase, User>(user);
        }

        public async Task<Either<NotificationBase, bool>> DeleteAnUserById(int id)
        {
            var updated = await userRepository.DeleteById(id);
            if (updated.IsLeft())
                return new Left<NotificationBase, bool>(updated.GetLeft());

            return new Right<NotificationBase, bool>(true);
        }
    }
}
