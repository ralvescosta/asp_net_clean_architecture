using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDbContext dbContext;
        public AuthorRepository(IDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Either<NotificationBase, Author>> SaveAuthor(Author author)
        {
            var sql = @"
                INSERT INTO authors 
                    (Guid, FirstName, LastName, Description, CreatedAt, UpdatedAt) VALUES 
                    (@Guid, @FirstName, @LastName, @Description, @CreatedAt, @UpdatedAt)";

            var parameters = new DynamicParameters();
            parameters.Add("@Guid", author.Guid, DbType.String);
            parameters.Add("@FirstName", author.FirstName, DbType.String);
            parameters.Add("@LastName", author.LastName, DbType.String);
            parameters.Add("@Description", author.Description, DbType.String);
            parameters.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql, parameters);
                return new Right<NotificationBase, Author>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Author>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, Author>> FindByName(string firstName, string lastName)
        {
            var sql = @"SELECT * FROM authors WHERE FirstName = @FirstName AND LastName = @LastName";

            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName, DbType.String);
            parameters.Add("@LastName", lastName, DbType.String);

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql, parameters);
                return new Right<NotificationBase, Author>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Author>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, Author>> FindById(int id)
        {
            var sql = @"SELECT * FROM authors WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql, parameters);
                return new Right<NotificationBase, Author>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Author>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, IEnumerable<Author>>> FindAll()
        {
            var sql = @"SELECT * FROM authors";

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql);
                return new Right<NotificationBase, IEnumerable<Author>>(result);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, IEnumerable<Author>>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, bool>> Update(Author author)
        {
            var sql = @"UPDATE authors SET";

            if (!string.IsNullOrEmpty(author.FirstName))
            {
                sql += $" FirstName = '{author.FirstName}',";
            }
            if (!string.IsNullOrEmpty(author.LastName))
            {
                sql += $" LastName = '{author.LastName}',";
            }
            if (!string.IsNullOrEmpty(author.Description))
            {
                sql += $" Description = '{author.Description}',";
            }
            sql = sql[0..^1];
            sql += $" WHERE Id = {author.Id}";

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql);
                return new Right<NotificationBase, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, bool>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, bool>> DeleteById(int id)
        {
            var sql = $"DELETE from authors WHERE Id = {id};";

            try
            {
                var result = await dbContext.QueryAsync<Author>(sql);
                return new Right<NotificationBase, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, bool>(new NotificationBase(ex.Message));
            }
        }
    }
}
