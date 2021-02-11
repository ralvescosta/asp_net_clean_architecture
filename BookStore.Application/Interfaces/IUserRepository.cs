using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
    }
}
