using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthenticationUseCase
    {
        Task<AuthenticatedUser> Auth(string token, Permissions permissionRequired);
    }
}
