using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yan.MvcClient.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpClient _client;

        public ArticleServiceClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<CategoryArticleCount>> GetArticleStaticCountByCategory()
        {
            var result= await _client.GetStringAsync("/api/articlemanage/Artilce/GetArticleStaticCountByCategory");

            var model = JsonConvert.DeserializeObject<ResultDto<List<CategoryArticleCount>>>(result);

            return model.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPage<ArticleListDto>> GetArticlePageByCategory(int categoryId,int index)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Artilce/GetArticlePageByCategory?categoryId={categoryId}&page={index}&rows=10");

            var model = JsonConvert.DeserializeObject<PageResultDto<ArticleListDto>>(result);

            return model.Result;
        }

    }
}
