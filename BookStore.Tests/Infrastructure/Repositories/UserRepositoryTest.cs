using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Infrastructure.Database;
using BookStore.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStore.Tests.Infrastructure.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private Mock<IDbConnectionFactory> dbConnFactory;
        private UserRepository userRepository;
        private User userMock;

        [TestInitialize]
        public void InitializeTest() 
        {
            dbConnFactory = new Mock<IDbConnectionFactory>();
            userRepository = new UserRepository(dbConnFactory.Object);
            userMock = new User()
            {
                Email = "user@email.com",
                PasswordHash = "hashed",
                Guid = Guid.NewGuid().ToString(),
                Name = "Joao",
                LastName = "Julios",
                Permission = Permissions.User
            };
        }

        [TestMethod]
        public async Task ShouldCreateUser()
        {
            dbConnFactory.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<User> { userMock }.AsEnumerable()));
            var result = await userRepository.SaveUser(userMock);
            Assert.AreEqual(result, userMock);
        }
        [TestMethod]
        public async Task ShouldFindByEmail()
        {
            dbConnFactory.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<User> { userMock }.AsEnumerable()));
            var result = await userRepository.FindByEmail(userMock.Email);
            Assert.AreEqual(result.Email, userMock.Email);
        }
        [TestMethod]
        public async Task ShouldFindById()
        {
            dbConnFactory.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<User> { userMock }.AsEnumerable()));
            var result = await userRepository.FindById(1);
            Assert.AreEqual(result.Email, userMock.Email);
        }

    }
}
