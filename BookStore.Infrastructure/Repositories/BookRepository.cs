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
    public class BookRepository : IBookRepository
    {
        private readonly IDbContext dbContext;
        public BookRepository(IDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Either<NotificationBase, Book>> Create(Book book)
        {
            var sql = @"
                INSERT INTO books 
                    (AuthorId, Guid, Title, Subtitle, Subject, CreatedAt, UpdatedAt) VALUES 
                    (@AuthorId, @Guid, @Title, @Subtitle, @Subject, @CreatedAt, @UpdatedAt)";

            var parameters = new DynamicParameters();
            parameters.Add("@AuthorId", book.AuthorId, DbType.Int32);
            parameters.Add("@Guid", book.Guid, DbType.String);
            parameters.Add("@Title", book.Title, DbType.String);
            parameters.Add("@Subtitle", book.Subtitle, DbType.String);
            parameters.Add("@Subject", book.Subject, DbType.String);
            parameters.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);

            try
            {
                var result = await dbContext.QueryAsync<Book>(sql, parameters);
                return new Right<NotificationBase, Book>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Book>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, IEnumerable<Book>>> FindAll()
        {
            var sql = @"SELECT * FROM books AS Book
                        LEFT JOIN authors AS Author WHERE Book.AuthorId = Author.Id";

            try
            {
                var result = await dbContext.QueryAsync<Book, Author, Book>(sql, (book, author) => 
                {
                    book.Author = author;
                    return book;
                });
                return new Right<NotificationBase, IEnumerable<Book>>(result);
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, IEnumerable<Book>>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, Book>> FindById(int id)
        {
            var sql = @"SELECT * FROM books AS Book
                        INNER JOIN authors AS Author ON Book.AuthorId = Author.Id
                        WHERE Book.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            try
            {
                var result = await dbContext.QueryAsync<Book, Author, Book>(sql, parameters, (book, author) =>
                {
                    book.Author = author;
                    return book;
                });
                return new Right<NotificationBase, Book>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Book>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, Book>> FindByTitle(string title)
        {
            var sql = @"SELECT * FROM books AS Book
                        INNER JOIN authors AS Author ON Book.AuthorId = Author.Id
                        WHERE Book.Title = @Title";

            var parameters = new DynamicParameters();
            parameters.Add("@Title", title, DbType.String);

            try
            {
                var result = await dbContext.QueryAsync<Book, Author, Book>(sql, parameters, (book, author) =>
                {
                    book.Author = author;
                    return book;
                });
                return new Right<NotificationBase, Book>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Left<NotificationBase, Book>(new NotificationBase(ex.Message));
            }
        }

        public async Task<Either<NotificationBase, bool>> Update(Book book)
        {
            var sql = @"UPDATE books SET";

            if (!string.IsNullOrEmpty(book.Title))
            {
                sql += $" Title = '{book.Title}',";
            }
            if (!string.IsNullOrEmpty(book.Subtitle))
            {
                sql += $" Subtitle = '{book.Subtitle}',";
            }
            if (!string.IsNullOrEmpty(book.Subject))
            {
                sql += $" Subject = '{book.Subject}',";
            }
            sql = sql[0..^1];
            sql += $" WHERE Id = {book.Id}";

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
            var sql = $"DELETE from books WHERE Id = @Id";

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
