using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Yan.Core.Dtos;
using Yan.MvcClient.ViewModel;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleOutputDto> GetArticleById(int id)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Artilce/GetArticleById/{id}");

            var model = JsonConvert.DeserializeObject<ResultDto<ArticleOutputDto>>(result);

            return model.Data;
        }

        public async Task<HandleResultDto> LikeIt(int id)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Artilce/LikeThisArticle/{id}");
            var model = JsonConvert.DeserializeObject<HandleResultDto>(result);
            return model;

        }

    }
}
