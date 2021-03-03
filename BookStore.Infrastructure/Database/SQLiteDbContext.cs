using Microsoft.Data.Sqlite;

namespace BookStore.Infrastructure.Database
{
    public class SQLiteDbContext
    {
        public readonly SqliteConnection DbContext;

        public SQLiteDbContext()
        {
            DbContext = new SqliteConnection();
        }

    }
}
