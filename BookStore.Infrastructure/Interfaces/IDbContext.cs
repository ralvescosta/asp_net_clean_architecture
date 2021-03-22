using System;
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

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, IDynamicParameters param, Func<TFirst, TSecond, TReturn> map);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string query, Func<TFirst, TSecond, TThird, TReturn> map);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string query, IDynamicParameters param, Func<TFirst, TSecond, TThird, TReturn> map);
    }
}
