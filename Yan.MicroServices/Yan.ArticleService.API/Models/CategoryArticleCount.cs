using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Models
{
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
}
