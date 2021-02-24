using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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
    }
}
