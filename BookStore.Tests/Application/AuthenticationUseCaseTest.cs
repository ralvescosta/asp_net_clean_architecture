using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
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
        public void ShouldThrowAplicationExceptionIfAuthorizationHeaderIsEmpty()
        {
            Assert.ThrowsExceptionAsync<ApplicationException>(() => authUseCase.Auth("", Permissions.User));
        }

        [TestMethod]
        public void ShouldThrowAplicationExceptionIfAuthorizationHeaderIsWrong()
        {
            Assert.ThrowsExceptionAsync<ApplicationException>(()=> authUseCase.Auth("token", Permissions.User));
        }

        [TestMethod]
        public void ShouldThrowUnauthorizedExcpetionIfTokenWasInvalid()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Throws(new Exception());
            Assert.ThrowsExceptionAsync<UnauthorizedExcpetion>(() => authUseCase.Auth("Bearer invalid_token", Permissions.User));
        }

        [TestMethod]
        public async Task ShouldReturnNullIfTheUserRoleDoNotSatisfied()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Returns(new TokenData() { Id = 1 });
            userRepository.Setup(m => m.FindById(It.IsAny<int>())).Returns(Task.FromResult(new User()));

            var result = await authUseCase.Auth("Bearer some_token", Permissions.Admin);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task ShouldReturnAuthenticaredUserIfEverthingIsFine()
        {
            tokenManagerService.Setup(m => m.VerifyToken(It.IsAny<string>())).Returns(new TokenData { Id = 1 });
            userRepository.Setup(m => m.FindById(It.IsAny<int>())).Returns(Task.FromResult(new User { Id = 1, Permission = Permissions.User, Email = "email@email.com", Guid = Guid.NewGuid() }));

            var result = await authUseCase.Auth("Bearer some_token", Permissions.User);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AuthenticatedUser));
        }
    }
}
