using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// Dapper 数据库上下文
    /// </summary>
    public abstract class DapperDbContext : IContext
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection _connection;

        /// <summary>
        /// 数据库事物
        /// </summary>
        private IDbTransaction _transaction;

        /// <summary>
        /// 上下文配置选项
        /// </summary>
        private readonly DapperDbContextOptions _options;

        /// <summary>
        /// 是否开始事物
        /// </summary>
        public bool IsTransactionBegin { get; private set; }

        /// <summary>
        /// 抽象方法，创建连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected abstract IDbConnection CreateConnection(string connectionString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        protected DapperDbContext(IOptions<DapperDbContextOptions> option)
        {
            _options = option.Value;
            _connection = CreateConnection(_options.Configuration);
            _connection.Open();
        }

        /// <summary>
        /// 开始事物
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
            IsTransactionBegin = true;
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
            IsTransactionBegin = false;
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        #region 

        /// <summary>
        /// 执行SQL，添加、删除、修改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType type = CommandType.Text)
        {

            return await _connection.ExecuteAsync(sql, param, _transaction, null, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(string sql, object param = null) where TEntity : class
        {
            return await _connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, _transaction, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name=""></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetList<TEntity>(string sql, object param = null)
        {
            return await _connection.QueryAsync<TEntity>(sql, param, _transaction, null);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (IsTransactionBegin)
            {
                Rollback();
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
