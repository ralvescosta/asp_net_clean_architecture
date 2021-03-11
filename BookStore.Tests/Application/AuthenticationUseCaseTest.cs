using BookStore.Application.Interfaces;
using BookStore.Application.Notifications;
using BookStore.Application.UseCase;
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
    public class AuthenticationUseCaseTest
    {
        private AuthenticationUseCase authUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<ITokenManagerService> tokenManagerService;

        [TestInitialize]
        public void TestInitialize() 
        {
            tokenManagerService = new Mock<ITokenManagerService>();
            userRepository = new Mock<IUserRepository>();
            authUseCase = new AuthenticationUseCase(tokenManagerService.Object, userRepository.Object);
        }

        [TestMethod]
        public async Task ShouldReturnUnauthorizedNotificationIfAuthorizationHeaderIsEmpty()
        {
            var result = await authUseCase.Auth("", Permissions.User);

            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(UnauthorizedNotification));
        }

        [TestMethod]
        public async Task ShouldReturnUnauthorizedNotificationIfAuthorizationHeaderIsWrong()
        {
            var result = await authUseCase.Auth("token", Permissions.User);

            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(UnauthorizedNotification));
        }

        [TestMethod]
        public async Task ShouldReturnUnauthorizedNotificationIfTokenWasInvalid()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Returns(new Left<NotificationBase, TokenData>(new NotificationBase("")));

            var result = await authUseCase.Auth("Bearer invalid_token", Permissions.User);

            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(UnauthorizedNotification));
        }

        [TestMethod]
        public async Task ShouldReturnUnauthorizedNotificationfTheUserRoleDoNotSatisfied()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Returns(new Right<NotificationBase, TokenData>(new TokenData { Id = 1 }));
            userRepository.Setup(m => m.FindById(It.IsAny<int>())).Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(new User { Id = 1 })));

            var result = await authUseCase.Auth("Bearer some_token", Permissions.Admin);

            Assert.IsTrue(result.IsLeft());
            Assert.IsInstanceOfType(result.GetLeft(), typeof(UnauthorizedNotification));
        }

        [TestMethod]
        public async Task ShouldReturnAuthenticaredUserIfEverthingIsFine()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Returns(new Right<NotificationBase, TokenData>(new TokenData { Id = 1 }));
            userRepository.Setup(m => m.FindById(It.IsAny<int>())).Returns(Task.FromResult<Either<NotificationBase, User>>(new Right<NotificationBase, User>(new User { Id = 1, Permission = Permissions.User, Email = "email@email.com", Guid = Guid.NewGuid().ToString() })));

            var result = await authUseCase.Auth("Bearer some_token", Permissions.User);

            Assert.IsTrue(result.IsRight());
            Assert.IsInstanceOfType(result.GetRight(), typeof(AuthenticatedUser));
        }
    }
}
