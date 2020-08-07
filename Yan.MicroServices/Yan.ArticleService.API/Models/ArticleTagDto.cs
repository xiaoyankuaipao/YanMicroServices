using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleTagDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ArticleCount { get; set; }
    }
}
