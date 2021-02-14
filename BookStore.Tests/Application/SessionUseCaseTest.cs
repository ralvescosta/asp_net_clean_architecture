﻿using BookStore.Application.Exceptions;
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
    public class SessionUseCaseTest
    {
        private SessionUseCase sessionUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<IHasher> hasher;
        private Mock<ITokenManager> tokenManager;
        private User userMock;
        private UserCredentials userCredentialsMock;
        
        [TestInitialize]
        public void TestInitialize()
        {
            userRepository = new Mock<IUserRepository>();
            hasher = new Mock<IHasher>();
            tokenManager = new Mock<ITokenManager>();
            sessionUseCase = new SessionUseCase(userRepository.Object, hasher.Object, tokenManager.Object);

            userMock = new User()
            {
                Email = "user@email.com",
                PasswordHash = "hashed",
                Guid = Guid.NewGuid(),
                Name = "Joao",
                LastName = "Julios",
                Permission = Permissions.User
            };
            userCredentialsMock = new UserCredentials() 
            { 
                Email = userMock.Email,
                Password = "123456"
            };
        }

        [TestMethod]
        public void ShouldTrhowNotFoundExceptionIfEmailNotFound()
        {
            userRepository.Setup(m => m.FindByEmail(It.IsAny<Email>())).Returns<User>(null);

            Assert.ThrowsExceptionAsync<NotFoundException>(() => sessionUseCase.CreateUserSession(userCredentialsMock));
        }

        [TestMethod]
        public void ShouldTrhowWrongPasswordExceptionIfPasswordNotMatch()
        {
            userRepository.Setup(m => m.FindByEmail(userMock.Email.ToString())).ReturnsAsync(userMock);
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            Assert.ThrowsExceptionAsync<WrongPasswordException>(() => sessionUseCase.CreateUserSession(userCredentialsMock));
        }

        [TestMethod]
        public void ShouldTrhowUnauthorizedExcpetionIfUserPermissionIsUnauthorized() 
        {
            userMock.Permission = Permissions.Unauthorized;
            userRepository.Setup(m => m.FindByEmail(userMock.Email.ToString())).ReturnsAsync(userMock);
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            Assert.ThrowsExceptionAsync<UnauthorizedExcpetion>(() => sessionUseCase.CreateUserSession(userCredentialsMock));
        }

        [TestMethod]
        public async Task ShouldRetornSessionIfSuccessfuly() 
        {
            userRepository.Setup(m => m.FindByEmail(userMock.Email.ToString())).ReturnsAsync(userMock);
            hasher.Setup(m => m.CompareHashe(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var result = await sessionUseCase.CreateUserSession(userCredentialsMock);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Session));
        }
    }
}
