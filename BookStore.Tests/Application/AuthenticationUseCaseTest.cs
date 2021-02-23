using BookStore.Application.UseCase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStore.Tests.Application
{
    [TestClass]
    public class AuthenticationUseCaseTest
    {
        private AuthenticationUseCase authUseCase;

        [TestInitialize]
        public void TestInitialize() 
        {
            authUseCase = new AuthenticationUseCase();
        }

        [TestMethod]
        public void ShouldThrowAplicationExceptionIfAuthorizationHeaderIsEmpty()
        {
            Assert.ThrowsExceptionAsync<ApplicationException>(() => authUseCase.Auth(""));
        }

        [TestMethod]
        public void ShouldThrowAplicationExceptionIfAuthorizationHeaderIsWrong()
        {
            Assert.ThrowsExceptionAsync<ApplicationException>(()=> authUseCase.Auth("token"));
        }
    }
}
