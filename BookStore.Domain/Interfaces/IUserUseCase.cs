using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IUserUseCase
    {
        Task<IEnumerable<User>> GetAllUsers(AuthenticatedUser auth);
    }
}
