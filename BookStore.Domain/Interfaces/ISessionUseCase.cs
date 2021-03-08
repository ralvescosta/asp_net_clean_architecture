using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ISessionUseCase
    {
        Task<Session> CreateUserSession(SessionRequestDTO credentials);
    }
}
