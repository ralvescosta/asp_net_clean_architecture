using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> SaveUser(User user);
        Task<User> FindByEmail(Email email);
        Task<User> FindById(int id);
        Task<IEnumerable<User>> FindAll();
    }
}
