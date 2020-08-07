using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.Dapper
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(string connectionString)
        {
            //var Configuration = new ConfigurationBuilder()
            //    .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            //    .Build();

            //_connectionString = Configuration["ConnectionStrings:MySqlConnection"];
            _connectionString = connectionString;
        }


        public async Task<int> ExecuteAsync(string path)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                using (StreamReader streamReader = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    var script = await streamReader.ReadToEndAsync();
                    return await connection.ExecuteAsync(script);
                }
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sql, param);
            }
        }

        public async Task<bool> ExecuteAsyncTransaction(List<string> list)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var sql in list)
                    {
                        await connection.ExecuteAsync(sql, null, transaction);
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    return false;
                }
            }
        }

        public async Task<bool> ExecuteAsyncTransaction(List<KeyValuePair<string, object>> list)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var item in list)
                    {
                        await connection.ExecuteAsync(item.Key, item.Value, transaction);
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    return false;
                }
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(sql, param);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null) where T : class
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        /// <summary>
        /// 分页查询：一次查询，将总数和列表一起返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<DapperPageResult<T>> QueryPage<T>(string sql, object parm = null) where T:class
        {
            DapperPageResult<T> result = new DapperPageResult<T>();
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                using (var reader = await connection.QueryMultipleAsync(sql, parm))
                {
                    result.Data = (await reader.ReadAsync<T>())?.ToList();
                    result.TotalCount = await reader.ReadFirstAsync<long>();
                }
            }

            return result;
        }
    }

}
