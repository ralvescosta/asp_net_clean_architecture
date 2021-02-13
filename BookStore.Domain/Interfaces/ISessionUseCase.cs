using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ISessionUseCase
    {
        Task<Session> CreateUserSession(UserCredentials credentials);
    }
}
