using Dapper;
using Microsoft.Extensions.Options;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class RepositoryService : IRepository
    {
        private readonly ConnectionString _options;

        public RepositoryService(IOptions<ConnectionString> options)
        {
            _options = options.Value;


        }
        public async Task<IEnumerable<T>> InvokeQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class
        {
            using (IDbConnection _connection = new SqlConnection(_options.DemoApiTest))
            {

                if (isStoreProcedure)
                    return await _connection.QueryAsync<T>(query, param, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.QueryAsync<T>(query, param);
            }
        }
        public T InvokeSingleQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class
        {
            using (IDbConnection _connection = new SqlConnection(_options.DemoApiTest))
            {

                if (isStoreProcedure)
                    return  _connection.QueryFirst<T>(query, param, null, null, CommandType.StoredProcedure);
                else
                    return  _connection.QueryFirst<T>(query, param);
            }
        }
        public async Task<IEnumerable<T>> InvokeQuery<T>(string query, bool isStoreProcedure = false) where T : class
        {
            using (IDbConnection _connection = new SqlConnection(_options.DemoApiTest))
            {

                if (isStoreProcedure)
                    return await _connection.QueryAsync<T>(query, null, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.QueryAsync<T>(query);
            }
        }


        public async Task<bool> InvokeExecute(string query, object param, bool isStoreProcedure = false)
        {
            using (IDbConnection _connection = new SqlConnection(_options.DemoApiTest))
            {

                if (isStoreProcedure)
                    await _connection.ExecuteAsync(query, param, null, null, CommandType.StoredProcedure);
                else
                    await _connection.ExecuteAsync(query, param);

                return true;
            }
        }
        public async Task<int> InvokeExecuteQuery(string query, object param, bool isStoreProcedure = false)
        {
            using (IDbConnection _connection = new SqlConnection(_options.DemoApiTest))
            {

                if (isStoreProcedure)
                    return await _connection.ExecuteScalarAsync<int>(query, param, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.ExecuteScalarAsync<int>(query, param);

            }
        }
    }
}
