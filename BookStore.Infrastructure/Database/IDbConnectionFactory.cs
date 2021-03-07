using System.Data;

namespace BookStore.Infrastructure.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
