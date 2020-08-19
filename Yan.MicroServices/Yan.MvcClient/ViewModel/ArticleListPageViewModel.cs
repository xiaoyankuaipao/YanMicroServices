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
        public string CategoryId { get; set; }
        public int PageIndex { get; set; }
        public int PageTotalCount { get; set; }
    }
}
