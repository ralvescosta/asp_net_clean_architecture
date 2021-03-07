namespace BookStore.Infrastructure.Database
{
    public interface IMigrations
    {
        void RunMigrate();
    }
}