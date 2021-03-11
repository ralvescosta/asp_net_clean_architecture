using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
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

        //[TestMethod]
        //public void ShouldThrowEmailAlreadyExistExceptionIfEmailAlreadyExist() 
        //{
        //    //Arranje
        //    userRepository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns(Task.FromResult(mockedUser));

        //    //Act and Assert
        //    Assert.ThrowsExceptionAsync<EmailAlreadyExistException>(() => registerUserUseCase.Register(mockedUserRegistration));
        //}

        [TestMethod]
        public void ShouldThrowApplicationExpectionIfSomeErrorOccurInFindByEmail()
        {
            //Arranje
            userRepository.Setup(m => m.FindByEmail(It.IsAny<string>())).Throws(new Exception());

            //Act and Assert
            Assert.ThrowsExceptionAsync<ApplicationException>(() => registerUserUseCase.Register(mockedUserRegistration));
        }

        [TestMethod]
        public void ShouldThrowApplicationExpectionIfSomeErrorOccurInSaveUser() 
        {
            //Arranje
            userRepository.Setup(m => m.SaveUser(It.IsAny<User>())).Throws(new Exception());

            //Act and Assert
            Assert.ThrowsExceptionAsync<ApplicationException>(() => registerUserUseCase.Register(mockedUserRegistration));
        }

        //[TestMethod]
        //public async Task ShouldReturnUserIfSuccess()
        //{
        //    //Arranje
        //    userRepository.Setup(m => m.FindByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
        //    userRepository.Setup(m => m.SaveUser(It.IsAny<User>())).Returns(Task.FromResult(mockedUser));

        //    //act
        //    var result = await registerUserUseCase.Register(mockedUserRegistration);

        //    //Act and Assert
        //    Assert.IsNotNull(result);
        //}

    }
}
