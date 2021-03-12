using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Application.UseCase;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace BookStore.Tests.Application
{
    [TestClass]
    public class SessionUseCaseTest
    {
        private SessionUseCase sessionUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<IHasherService> hasher;
        private Mock<ITokenManagerService> tokenManagerService;
        private Mock<IConfigurations> configurations;
        private User userMock;
        private SessionRequestDTO userCredentialsMock;
        
        [TestInitialize]
        public void TestInitialize()
        {
            userRepository = new Mock<IUserRepository>();
            hasher = new Mock<IHasherService>();
            tokenManagerService = new Mock<ITokenManagerService>();
            configurations = new Mock<IConfigurations>();
            sessionUseCase = new SessionUseCase(userRepository.Object, hasher.Object, tokenManagerService.Object, configurations.Object);

            userMock = new User()
            {
                Email = "user@email.com",
                PasswordHash = "hashed",
                Guid = Guid.NewGuid().ToString(),
                Name = "Joao",
                LastName = "Julios",
                Permission = Permissions.User
            };
            userCredentialsMock = new SessionRequestDTO() 
            { 
                Email = userMock.Email,
                Password = "123456"
            };
        }

        [TestMethod]
        public async Task ShouldReturnNotFoundNotificationIfEmailNotFound()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(null)));

            // Act
            var result = await sessionUseCase.CreateUserSession(userCredentialsMock);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(NotFoundNotification));
        }

        [TestMethod]
        public async Task ShouldReturnWrongPasswordNotificationIfPasswordNotMatch()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(userMock.Email))
               .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(userMock)));
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            // Act
            var result = await sessionUseCase.CreateUserSession(userCredentialsMock);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(WrongPasswordNotification));
        }

        [TestMethod]
        public async Task ShouldReturnUnauthorizedNotificationIfUserPermissionIsUnauthorized()
        {
            // Arrange
            userMock.Permission = Permissions.Unauthorized;
            userRepository.Setup(m => m.FindByEmail(userMock.Email))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(userMock)));
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            // Act
            var result = await sessionUseCase.CreateUserSession(userCredentialsMock);

            // Assert
            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(UnauthorizedNotification));
        }

        [TestMethod]
        public async Task ShouldRetornSessionIfSuccessfuly()
        {
            // Arrange
            userRepository.Setup(m => m.FindByEmail(userMock.Email))
                .Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(userMock)));
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            // Act
            var result = await sessionUseCase.CreateUserSession(userCredentialsMock);

            // Assert
            Assert.IsTrue(result.IsRight());
            Assert.IsInstanceOfType(result.GetRight(), typeof(Session));
        }
    }
}
