using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<UserModel> usersModel;
        public UserRepository() 
        {
            usersModel = new List<UserModel>();
        }

        public Task<User> CreateUser(User user)
        {
            var userModel = new UserModel()
            {
                Guid = user.Guid,
                Name = user.Name,
                LastName = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Permission = user.Permission,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            usersModel.Add(userModel);

            return Task.FromResult(user);
        }

        public Task<User> FindByEmail(Email email) 
        {
            var user = usersModel
                    .Where(u => u.Email.ToString() == email.ToString())
                    .Select(u => new User() 
                    {
                        Guid = u.Guid,
                        Name = u.Name,
                        LastName = u.Name,
                        Email = u.Email,
                        PasswordHash = u.PasswordHash,
                        Permission = u.Permission,
                    })
                    .FirstOrDefault();
            return Task.FromResult(user);
        }

    }
}
