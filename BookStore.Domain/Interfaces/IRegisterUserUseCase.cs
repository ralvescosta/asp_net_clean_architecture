using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IRegisterUserUseCase
    {
        Task<User> Register(UserRegistrationDTO user);
    }
}
