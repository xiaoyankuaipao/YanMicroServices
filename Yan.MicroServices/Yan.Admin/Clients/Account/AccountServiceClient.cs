using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yan.Admin.Models.UserLogin;
using Yan.Core.Base;
using Yan.Core.Base.Enum;

namespace Yan.Admin.Clients.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        private const string Loginapi = "/api/identityservice/Login/RequestToken";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public AccountServiceClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> LoginInAsync(LoginDto loginDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(Loginapi, content);
            if (result.IsSuccessStatusCode)
            {
                var resultStr = await result.Content.ReadAsStringAsync();
                var resultObj = JsonConvert.DeserializeObject<LoginResult>(resultStr);
                if (resultObj.State == 0)
                {
                    return new ServiceResult(ServiceResultCode.Failed);
                }

                var serviceResult = new ServiceResult(ServiceResultCode.Succeed);
                serviceResult.Data = resultObj.Token;
                return serviceResult;
            }

            return new ServiceResult(ServiceResultCode.Error);
        }

    }
}
