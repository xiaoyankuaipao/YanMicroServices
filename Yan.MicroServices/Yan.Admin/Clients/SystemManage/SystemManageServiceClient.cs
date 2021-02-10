using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yan.Core.Dtos;

namespace Yan.Admin.Clients.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemManageServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public SystemManageServiceClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        public const string GET_USER_INFO = "/api/systemmanageservice/User/GetUserInfo";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public async Task<UserInfo> GetUserInfo(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await _client.GetStringAsync(GET_USER_INFO);
            var model = JsonConvert.DeserializeObject<ResultDto<UserInfo>>(result);
            return model.Data;
        }

    }
}
