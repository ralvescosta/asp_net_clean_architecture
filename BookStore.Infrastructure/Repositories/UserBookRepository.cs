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
    public class UserBookRepository : IUserBookRepository
    {
        private readonly IDbContext dbContext;
        public UserBookRepository(IDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Either<NotificationBase, UserBook>> SaveUserBook(UserBook userBook)
        {
            var sql = @"
                INSERT INTO users_books 
                    (Guid, UserId, BookId, ExpiredAt, CreatedAt, UpdatedAt) VALUES 
                    (@Guid, @UserId, @BookId, @ExpiredAt, @CreatedAt, @UpdatedAt)";

            var parameters = new DynamicParameters();
            parameters.Add("@Guid", userBook.Guid, DbType.String);
            parameters.Add("@UserId", userBook.UserId, DbType.Int32);
            parameters.Add("@BookId", userBook.BookId, DbType.Int32);
            parameters.Add("@ExpiredAt", userBook.ExpiredAt, DbType.DateTime);
            parameters.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);

            try
            {
                var result = await dbContext.QueryAsync<UserBook>(sql, parameters);
                return new Right<NotificationBase, UserBook>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, UserBook>(new NotificationBase(ex.Message));
            }
        }
        public async Task<Either<NotificationBase, UserBook>> FindById(int id)
        {
            var sql = @"SELECT * FROM users_books AS UserBook
                        INNER JOIN users AS User ON UserBook.UserId = User.Id
                        INNER JOIN books AS Book ON UserBook.BookId = Book.Id
                        WHERE UserBook.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            try
            {
                var result = await dbContext.QueryAsync<UserBook, User, Book, UserBook>(sql, parameters, (userBook, user, book) =>
                {
                    userBook.User = user;
                    userBook.Book = book;
                    return userBook;
                });
                return new Right<NotificationBase, UserBook>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, UserBook>(new NotificationBase(ex.Message));
            }
        }
        public async Task<Either<NotificationBase, UserBook>> FindByBookId(int id)
        {
            var sql = @"SELECT * FROM users_books AS UserBook
                        INNER JOIN users AS User ON UserBook.UserId = User.Id
                        INNER JOIN books AS Book ON UserBook.BookId = Book.Id
                        WHERE UserBook.BookId = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            try
            {
                var result = await dbContext.QueryAsync<UserBook, User, Book, UserBook>(sql, parameters, (userBook, user, book) =>
                {
                    userBook.User = user;
                    userBook.Book = book;
                    return userBook;
                });
                return new Right<NotificationBase, UserBook>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, UserBook>(new NotificationBase(ex.Message));
            }
        }
        public async Task<Either<NotificationBase, IEnumerable<UserBook>>> FindAll()
        {
            var sql = @"SELECT * FROM users_books AS UserBook
                        INNER JOIN users AS User ON UserBook.UserId = User.Id
                        INNER JOIN books AS Book ON UserBook.BookId = Book.Id";

            try
            {
                var result = await dbContext.QueryAsync<UserBook, User, Book, UserBook>(sql, (userBook, user, book) =>
                {
                    userBook.User = user;
                    userBook.Book = book;
                    return userBook;
                });
                return new Right<NotificationBase, IEnumerable<UserBook>>(result);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, IEnumerable<UserBook>>(new NotificationBase(ex.Message));
            }
        }
        public async Task<Either<NotificationBase, bool>> Update(UserBook userBook)
        {
            var sql = @"UPDATE users_books SET ExpiredAt = @ExpiredAt
                        WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", userBook.Id, DbType.Int32);
            parameters.Add("@ExpiredAt", userBook.ExpiredAt, DbType.DateTime);

            try
            {
                var result = await dbContext.QueryAsync<Book>(sql);
                return new Right<NotificationBase, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, bool>(new NotificationBase(ex.Message));
            }
        }
        public async Task<Either<NotificationBase, bool>> DeleteById(int id)
        {
            var sql = $"DELETE from users_books WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            try
            {
                var result = await dbContext.QueryAsync<Book>(sql, parameters);
                return new Right<NotificationBase, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, bool>(new NotificationBase(ex.Message));
            }
        }
    }
}
