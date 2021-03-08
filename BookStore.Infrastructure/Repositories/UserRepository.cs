using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using BookStore.Infrastructure.Interfaces;

namespace BookStore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext dbContext;
        public UserRepository(IDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<User> SaveUser(User user)
        {
            var sql = @"
                INSERT INTO users 
                    (Guid, Name, LastName, Email, PasswordHash, Permission, CreatedAt, UpdatedAt) VALUES 
                    (@Guid, @Name, @LastName, @Email, @PasswordHash, @Permission, @CreatedAt, @UpdatedAt)";

            var parameters = new DynamicParameters();
            parameters.Add("@Guid", user.Guid, DbType.Guid);
            parameters.Add("@Name", user.Name, DbType.String);
            parameters.Add("@LastName", user.LastName, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("@Permission", user.Permission, DbType.Int16);
            parameters.Add("@CreatedAt", DateTime.Now, DbType.DateTime);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);

            var result = await dbContext.QueryAsync<User>(sql, parameters);
            return result.FirstOrDefault();
        }

        public async Task<User> FindByEmail(string email) 
        {
            var sql = @"SELECT * FROM users WHERE email = @Email";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            var result = await dbContext.QueryAsync<User>(sql, parameters);
            return result.FirstOrDefault();
        }

        public async Task<User> FindById(int id)
        {
            var sql = @"SELECT * FROM users WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", id, DbType.String);

            var result = await dbContext.QueryAsync<User>(sql, parameters);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            var sql = @"SELECT * FROM users";

            var result = await dbContext.QueryAsync<User>(sql);
            return result;
        }
    }
}
