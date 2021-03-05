using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;

namespace BookStore.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository userRepository;
        public UserUseCase(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsers(AuthenticatedUser auth)
        {
            var users = await userRepository.FindAll();
            return users;
        }
    }
}
