using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace BookStore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<UserModel> usersModel;
        public UserRepository() 
        {
            usersModel = new List<UserModel>();
        }
        public void CreateUser(User user)
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
        }
    }
}
