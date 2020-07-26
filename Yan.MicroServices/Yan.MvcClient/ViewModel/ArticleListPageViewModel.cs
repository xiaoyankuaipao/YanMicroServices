using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.MvcClient.Clients;

namespace Yan.MvcClient.ViewModel
{
    public class ArticleListPageViewModel
    {
        public ResultPage<ArticleListDto> ResultPage { get; set; }
        public int CategoryId { get; set; }
        public int PageIndex { get; set; }
        public int PageTotalCount { get; set; }
    }
}
