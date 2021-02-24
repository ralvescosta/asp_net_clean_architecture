using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> SaveUser(User user);
        Task<User> FindByEmail(Email email);
        Task<User> FindById(int id);
    }
}
