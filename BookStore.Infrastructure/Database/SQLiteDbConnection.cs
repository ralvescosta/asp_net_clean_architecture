using Microsoft.Data.Sqlite;
using System.Data;

namespace BookStore.Infrastructure.Database
{
    public class SQLiteDbConnection : IDbConnectionFactory
    {
        private readonly IDbConnection dbConn = new SqliteConnection("Data Source=SQLiteTutorialsDB.db");
        public IDbConnection GetConnection()
        {
            if (dbConn.State == ConnectionState.Closed)
                dbConn.Open();

            return dbConn;
        }
    }
}
