﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string CategoryId { get; set; }

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
        public string Id { get; set; }

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
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryId { get; set; }

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

    public class MessageOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }
    }

    public class MessageCreateDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class CreateMessagDto
    {
        [Required]
        [StringLength(1000,MinimumLength =5,ErrorMessage ="留言的长度要大于5")]
        public string Message { get; set; }
    }
}
