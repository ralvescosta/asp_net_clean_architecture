using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthenticationUseCase
    {
        Task<AuthenticatedUser> Auth(string token);
    }
}
