using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.Application
{
    [TestClass]
    public class UserUseCaseTest
    {
        private UserUseCase userUsecase;
        private Mock<IUserRepository> userRepository;
        private User userMock;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepository = new Mock<IUserRepository>();
            userUsecase = new UserUseCase(userRepository.Object);
            userMock = new User
            {
                Id = 1,
                Email = "user@email.com",
                PasswordHash = "hashed",
                Guid = Guid.NewGuid().ToString(),
                Name = "Joao",
                LastName = "Julios",
                Permission = Permissions.User
            };
        }

        [TestMethod]
        public async Task GetAllUsersShouldReturnListOfUserIfSuccess()
        {
            // Arrange
            userRepository.Setup(m => m.FindAll())
                .Returns(Task.FromResult<Either<NotificationBase, IEnumerable<User>>>(new Right<NotificationBase, IEnumerable<User>>(new List<User> { userMock })));

            // Act
            var users = await userUsecase.GetAllUsers();

            // Assert
            Assert.IsTrue(users.IsRight());
            Assert.AreEqual(users.GetRight().FirstOrDefault(), userMock);
        }

        [TestMethod]
        public async Task GetAllUsersShouldReturnNotificationBaseIfSomethingWrong()
        {
            // Arrange
            userRepository.Setup(m => m.FindAll())
                .Returns(Task.FromResult<Either<NotificationBase, IEnumerable<User>>>(new Left<NotificationBase, IEnumerable<User>>(new NotificationBase(""))));

            // Act
            var users = await userUsecase.GetAllUsers();

            // Assert
            Assert.IsTrue(users.IsLeft());
            Assert.IsInstanceOfType(users.GetLeft(), typeof(NotificationBase));
        }

        [TestMethod]
        public async Task GetAnUsersShouldReturnUserIfSuccess()
        {
            // Arrange
            userRepository.Setup(m => m.FindById(It.IsAny<int>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(userMock)));

            // Act
            var users = await userUsecase.GetAnUserById(userMock.Id);

            // Assert
            Assert.IsTrue(users.IsRight());
            Assert.AreEqual(users.GetRight(), userMock);
        }

        [TestMethod]
        public async Task GetAnUsersShouldReturnNotificationBaseIfSomethingWrong()
        {
            // Arrange
            userRepository.Setup(m => m.FindById(It.IsAny<int>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Left<NotificationBase, User>(new NotificationBase(""))));

            // Act
            var users = await userUsecase.GetAnUserById(userMock.Id);

            // Assert
            Assert.IsTrue(users.IsLeft());
            Assert.IsInstanceOfType(users.GetLeft(), typeof(NotificationBase));
        }
    }
}
