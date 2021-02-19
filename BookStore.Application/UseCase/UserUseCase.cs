using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BookStore.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        public Task<IEnumerable<User>> GetAllUsers(AuthenticatedUser auth)
        {
            var users = new List<User>().AsEnumerable();
            return Task.FromResult(users);
        }
    }
}
