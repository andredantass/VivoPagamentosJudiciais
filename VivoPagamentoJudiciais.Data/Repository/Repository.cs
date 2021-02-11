using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Data.Interfaces;

namespace VivoPagamentoJudiciais.Data.Repository
{
    public class Repository : IDisposable, IRepository
    {
        protected SqlConnection _connection;
        public IConfiguration _configuration;
        public IMemoryCache _cache;


        public Repository(IConfiguration configuration, IMemoryCache cache)
        {
            _configuration = configuration;
            _cache = cache;

            _connection = new SqlConnection(_configuration.GetConnectionString("BRSBESQLDEV"));
        }

        public async virtual Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, DynamicParameters parameters) 
        {
            return await _connection.QueryAsync<TEntity>(query, parameters);
        }

        public async virtual Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, string splitOn, DynamicParameters parameters)
        {

            return await _connection.QueryAsync(query, map, parameters, splitOn: splitOn);
        }

        public async virtual Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn, DynamicParameters parameters)
        {

            return await _connection.QueryAsync(query, map, parameters, splitOn: splitOn);
        }


        public async virtual Task<TEntity> QueryFirstAsync<TEntity>(string query, DynamicParameters parameters)
        {
            return await _connection.QueryFirstAsync<TEntity>(query, parameters);
        }


        public void Dispose()
        {
            ReleaseConnection();
        }


        public void ReleaseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        protected void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }
    }
}
