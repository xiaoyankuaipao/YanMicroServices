using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(string sql, TEntity entity = null)
        {
            return await _dbContext.ExecuteAsync(sql, entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string sql, int id)
        {
            return await _dbContext.ExecuteAsync(sql, new { Id = id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(string sql, TEntity entity = null)
        {
            return await _dbContext.ExecuteAsync(sql, entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefaultAsync(string sql, object param)
        {
            return await _dbContext.GetFirstOrDefaultAsync<TEntity>(sql, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(string sql, object param)
        {
            var result = await _dbContext.GetList<TEntity>(sql, param);
            return result.AsList();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }


    }
}
