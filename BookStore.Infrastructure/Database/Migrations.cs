namespace BookStore.Infrastructure.Database
{
    public class Migrations
    {
        private readonly SQLiteDbContext dbContext;
        public Migrations(SQLiteDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
    }
}
