using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(string sql, TEntity entity = null);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string sql, int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(string sql, TEntity entity = null);

        /// <summary>
        /// 查询满足条件的第一个实体,没有找到话返回null
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetFirstOrDefaultAsync(string sql, object param = null);

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(string sql, object param = null);



    }

}