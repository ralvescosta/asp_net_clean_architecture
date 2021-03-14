using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;
using BookStore.Shared.Utils;
using BookStore.Shared.Notifications;

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

            return new Right<NotificationBase, User>(user.GetRight());
        }
    }
}
