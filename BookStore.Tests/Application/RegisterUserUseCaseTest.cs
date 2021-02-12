using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BookStore.Tests.Application
{
    [TestClass]
    public class RegisterUserUseCaseTest
    {
        private RegisterUserUseCase registerUserUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<IHasher> hasher;
        private User mockedUser;

        [TestInitialize]
        public void TestInitialize() 
        {
            userRepository = new Mock<IUserRepository>();
            hasher = new Mock<IHasher>();
            registerUserUseCase = new RegisterUserUseCase(userRepository.Object, hasher.Object);
            mockedUser = new User()
            {
                Guid = Guid.NewGuid(),
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                PasswordHash = "PasswordHash",
                Permission = Permissions.User,
            };
        }

        [TestMethod]
        public void ShouldThrowEmailAlreadyExistExceptionIfEmailAlreadyExist() 
        {
            //Arranje
            userRepository.Setup(m => m.FindByEmail(It.IsAny<Email>())).Returns(mockedUser);
            var input = new UserRegistrationDTO()
            {
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                Password = "Password"
            };

            //Act and Assert
            Assert.ThrowsException<EmailAlreadyExistException>(() => registerUserUseCase.Register(input));
        }

        [TestMethod]
        public void ShouldThrowApplicationExpectionIfSomeErrorOccur() 
        {
            //Arranje
            userRepository.Setup(m => m.CreateUser(It.IsAny<User>())).Throws(new Exception());
            var input = new UserRegistrationDTO()
            {
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                Password = "Password"
            };

            //Act and Assert
            Assert.ThrowsException<ApplicationException>(() => registerUserUseCase.Register(input));
        }

        [TestMethod]
        public void ShouldReturnUserIfSuccess()
        {
            //Arranje
            userRepository.Setup(m => m.FindByEmail(It.IsAny<Email>())).Returns<User>(null);
            userRepository.Setup(m => m.CreateUser(It.IsAny<User>()));
            var input = new UserRegistrationDTO()
            {
                Name = "Fulano",
                LastName = "DeTal",
                Email = "fulano@detal.com",
                Password = "Password"
            };

            //act
            var result = registerUserUseCase.Register(input);

            //Act and Assert
            Assert.IsNotNull(result);
        }

    }
}
