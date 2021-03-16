using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStore.Tests.Infrastructure.Repositories
{
    [TestClass]
    public class AuthorRepositoryTest
    {
        private Mock<IDbContext> dbConnFactory;
        private AuthorRepository authorRepository;
        private Author authorMock;

        [TestInitialize]
        public void InitializeTest() 
        {
            dbConnFactory = new Mock<IDbContext>();
            authorRepository = new AuthorRepository(dbConnFactory.Object);
            authorMock = new Author
            {
                Id = 1,
                FirstName = "Joao",
                LastName = "Silva",
                Description = "Author",
                Guid = Guid.NewGuid().ToString()
            };
        }

        [TestMethod]
        public async Task ShouldReturnUserIfCreatedSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.SaveAuthor(authorMock);
            Assert.AreEqual(result.GetRight(), authorMock);
        }

        [TestMethod]
        public async Task ShouldReturnUserIfFindByGuidSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.FindByName(authorMock.FirstName, authorMock.LastName);
            Assert.AreEqual(result.GetRight().Guid, authorMock.Guid);
        }

        [TestMethod]
        public async Task ShouldReturnUserIfFindByIdSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.FindById(authorMock.Id);
            Assert.AreEqual(result.GetRight().Guid, authorMock.Guid);
        }

        [TestMethod]
        public async Task ShouldReturnAllUsersIfFindAllSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.FindAll();
            Assert.AreEqual(result.GetRight().FirstOrDefault(), authorMock);
        }

        [TestMethod]
        public async Task ShouldReturnTrueIfUpdatedSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.Update(new Author { Id = 1, FirstName = "Manolo" });
            Assert.IsTrue(result.IsRight());
        }

        [TestMethod]
        public async Task ShouldReturnTrueIfDeletedSuccesfuly()
        {
            dbConnFactory.Setup(m => m.QueryAsync<Author>(It.IsAny<string>(), It.IsAny<IDynamicParameters>())).Returns(Task.FromResult(new List<Author> { authorMock }.AsEnumerable()));
            var result = await authorRepository.DeleteById(1);
            Assert.IsTrue(result.IsRight());
        }
    }
}
