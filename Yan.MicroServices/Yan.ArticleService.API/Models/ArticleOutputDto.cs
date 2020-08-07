using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.ArticleService.API.Models
{
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

}
