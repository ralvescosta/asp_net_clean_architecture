using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStore.Infrastructure.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
        Task<IEnumerable<T>> QueryAsync<T>(string query);
        Task<IEnumerable<T>> QueryAsync<T>(string query, IDynamicParameters param);
    }
}
