using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Application.UseCase;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace BookStore.Tests.Application
{
    [TestClass]
    public class RegisterUserUseCaseTest
    {
        private RegisterUserUseCase registerUserUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<IHasherService> hasher;
        private User mockedUser;
        private UserRegistrationRequestDTO mockedUserRegistration;

        [TestInitialize]
        public void TestInitialize() 
        {
            userRepository = new Mock<IUserRepository>();
            hasher = new Mock<IHasherService>();
            registerUserUseCase = new RegisterUserUseCase(userRepository.Object, hasher.Object);
            mockedUser = new User()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                PasswordHash = "PasswordHash",
                Permission = Permissions.User,
            };

            mockedUserRegistration = new UserRegistrationRequestDTO()
            {
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                Password = "Password"
            };
        }

        [TestMethod]
        public async Task ShouldReturnsEmailAlreadyNotificationIfEmailAlreadyExist()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(mockedUser)));

            // Act
            var result = await registerUserUseCase.Register(mockedUserRegistration);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(EmailAlreadyExistNotification));
        }

        [TestMethod]
        public async Task ShouldReturnNotificationBaseIfSomeErrorOccurInFindByEmail()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Left<NotificationBase, User>(new NotificationBase(""))));

            // Act
            var result = await registerUserUseCase.Register(mockedUserRegistration);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(NotificationBase));
        }

        [TestMethod]
        public async Task ShouldReturnNotificationBaseIfSomeErrorOccurInSaveUser() 
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(null)));
            userRepository.Setup(m => m.SaveUser(It.IsAny<User>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Left<NotificationBase, User>(new NotificationBase(""))));

            // Act
            var result = await registerUserUseCase.Register(mockedUserRegistration);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(NotificationBase));
        }

        [TestMethod]
        public async Task ShouldReturnUserIfSuccess()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(null)));
            userRepository.Setup(m => m.SaveUser(It.IsAny<User>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(mockedUser)));

            // Act
            var result = await registerUserUseCase.Register(mockedUserRegistration);

            // Assert
            Assert.IsTrue(result.IsRight());
            Assert.AreEqual(result.GetRight().Email, mockedUser.Email);
        }

    }
}
