using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.Clients
{
    /// <summary>
    /// 查询输出
    /// </summary>
    public class ResultDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CategoryArticleCount
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ArticleCount { get; set; }
    }

    /// <summary>
    /// 分页查询输出
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ResultPage<T> Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultPage<T>
    {
        public int TotalCount { get; set; }

        public IList<T> Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleListDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ArticleDto ArticleDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> TagNames { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LikeCount { get; set; }
    }
}
