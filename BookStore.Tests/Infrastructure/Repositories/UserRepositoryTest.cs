using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BookStore.Tests.Infrastructure.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository userRepository;
        private User userMock;

        [TestInitialize]
        public void InitializeTest() 
        {
            userRepository = new UserRepository();
            userMock = new User()
            {
                Email = "user@email.com",
                PasswordHash = "hashed",
                Guid = Guid.NewGuid(),
                Name = "Joao",
                LastName = "Julios",
                Permission = Permissions.User
            };
        }

        [TestMethod]
        public async Task ShouldCreateUser()
        {
            var result = await userRepository.SaveUser(userMock);
            Assert.AreEqual(result, userMock);
        }
        [TestMethod]
        public async Task ShouldFindByEmail()
        {
            await userRepository.SaveUser(userMock);
            var result = await userRepository.FindByEmail(userMock.Email);
            Assert.AreEqual(result.Email, userMock.Email);
        }

    }
}
