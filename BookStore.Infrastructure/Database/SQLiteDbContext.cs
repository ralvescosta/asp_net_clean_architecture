﻿using BookStore.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BookStore.Infrastructure.Database
{
    public class SQLiteDbContext : IDbContext
    {
        private readonly IDbConnection dbConn;
        public SQLiteDbContext()
        {
            dbConn = new SqliteConnection("Data Source=BookStore.db");
            if (dbConn.State == ConnectionState.Closed)
                dbConn.Open();
        }

        public IDbConnection GetConnection()
        {
            if (dbConn.State == ConnectionState.Closed)
                dbConn.Open();

            return dbConn;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string query)
        {
            return dbConn.QueryAsync<T>(query);
        }
        public Task<IEnumerable<T>> QueryAsync<T>(string query, IDynamicParameters param)
        {
            return dbConn.QueryAsync<T>(query, param);
        }
    }
}
