using Dapper;
using Microsoft.Data.Sqlite;

namespace BookStore.Infrastructure.Database
{
    public class Migrations : IMigrations
    {
        public void RunMigrate()
        {
            using var dbConnection = new SqliteConnection("Data Source=SQLiteTutorialsDB.db");

            dbConnection.Execute("Create Table users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Guid VARCHAR(36) NOT NULL, Name VARCHAR(80) NOT NULL, LastName VARCHAR(80) NOT NULL, Email VARCHAR(255) NOT NULL, PasswordHash VARCHAR(255) NOT NULL, Permission INTEGER NOT NULL, CreatedAt TIMESTAMP NOT NULL, UpdatedAt TIMESTAMP NOT NULL)");
        }
    }
}
