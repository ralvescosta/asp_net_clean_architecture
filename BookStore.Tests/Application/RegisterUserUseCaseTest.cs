using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
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
    public class RegisterUserUseCaseTest
    {
        private RegisterUserUseCase registerUserUseCase;
        private Mock<IUserRepository> userRepository;
        private Mock<IHasher> hasher;

        [TestInitialize]
        public void TestInitialize() 
        {
            userRepository = new Mock<IUserRepository>();
            hasher = new Mock<IHasher>();
            registerUserUseCase = new RegisterUserUseCase(userRepository.Object, hasher.Object);
        }

        [TestMethod]
        public void ShouldThrowEmailAlreadyExistExceptionIfEmailAlreadyExist() 
        { 

        }

    }
}
