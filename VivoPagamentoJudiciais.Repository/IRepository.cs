using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoPagamentoJudiciais.Repository
{
    public interface IRepository : IDisposable
    {
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, DynamicParameters parameters);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, string splitOn, DynamicParameters parameters);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn, DynamicParameters parameters);

        Task<TEntity> QueryFirstAsync<TEntity>(string query, DynamicParameters parameters);
    }
}
