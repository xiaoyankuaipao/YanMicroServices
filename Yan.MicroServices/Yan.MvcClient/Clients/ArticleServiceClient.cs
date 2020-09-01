using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public ArticleServiceClient(HttpClient client)
        {
            _client = client;
        }

        #region Article

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryArticleCount>> GetArticleStaticCountByCategory()
        {
            var result = await _client.GetStringAsync("/api/articlemanage/Artilce/GetArticleStaticCountByCategory");

            var model = JsonConvert.DeserializeObject<ResultDto<List<CategoryArticleCount>>>(result);

            return model.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPage<ArticleListDto>> GetArticlePageByCategory(string categoryId, int index)
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
        public async Task<ArticleOutputDto> GetArticleById(string id)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Artilce/GetArticleById/{id}");

            var model = JsonConvert.DeserializeObject<ResultDto<ArticleOutputDto>>(result);

            return model.Data;
        }

        public async Task<HandleResultDto> LikeIt(string id)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Artilce/LikeThisArticle/{id}");
            var model = JsonConvert.DeserializeObject<HandleResultDto>(result);
            return model;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<PageResultDto<MessageOutputDto>> GetMessagePage(int page, int size)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Message/GetMessagePage?page={page}&size={size}&rows=10");

            var model = JsonConvert.DeserializeObject<PageResultDto<MessageOutputDto>>(result);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<PageResultDto<MessageOutputDto>> GetMessageSkipPage(int skip, int size)
        {
            var result = await _client.GetStringAsync($"/api/articlemanage/Message/GetMessageSkipPage?skip={skip}&size={size}");

            var model = JsonConvert.DeserializeObject<PageResultDto<MessageOutputDto>>(result);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> AddMessage(MessageCreateDto input)
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var result = await _client.PostAsync("/api/articlemanage/Message/AddMessage", content);

            if (result.IsSuccessStatusCode)
            {
                var responseStr = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<HandleResultDto>(responseStr);
                return response;
            }
            else
            {
                return new HandleResultDto
                {
                    State = 0,
                };
            }
        }

        #endregion

    }

}
