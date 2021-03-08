using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStore.Infrastructure.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
        Task<IEnumerable<T>> QueryAsync<T>(string query);
        Task<IEnumerable<T>> QueryAsync<T>(string query, IDynamicParameters param);
    }
}
