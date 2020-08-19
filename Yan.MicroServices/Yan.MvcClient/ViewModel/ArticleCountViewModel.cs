using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.ViewModel
{
    public class ArticleCountViewModel
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ArticleCount { get; set; }
    }
}
