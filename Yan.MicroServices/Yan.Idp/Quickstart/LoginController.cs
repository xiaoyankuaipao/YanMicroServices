﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yan.Idp.Models.ApiModels;

namespace Yan.Idp.Quickstart
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/identityservice/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IHttpClientFactory HttpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }


        /// <summary>
        /// 资源所有者密码凭据授权类型的用户获取Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<LoginResult> RequestToken([FromBody]LoginRequestParam model)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = model.ClientId;
            dict["client_secret"] = "spasecret";
            dict["grant_type"] = "password";
            dict["username"] = model.UserName;
            dict["password"] = model.Password;

            LoginResult result = new LoginResult();

            var httpClient = HttpClientFactory.CreateClient();
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await httpClient.PostAsync("http://localhost:5100/connect/token", content);
                if (!msg.IsSuccessStatusCode)
                {
                    //return StatusCode(Convert.ToInt32(msg.StatusCode));
                    return result;
                }

                string response = await msg.Content.ReadAsStringAsync();
                result.State = 1;
                result.Token = JsonConvert.DeserializeObject<Token>(response);

                //return Content(result, "application/json");
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<LoginResult> RefreshToken([FromBody] RefreshTokenRequestParam model)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = model.ClientId;
            dict["client_secret"] = "spasecret";
            dict["grant_type"] = "refresh_token";
            dict["refresh_token"] = model.RefreshToken;

            LoginResult result = new LoginResult();

            var httpClient = HttpClientFactory.CreateClient();
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await httpClient.PostAsync("http://localhost:5100/connect/token", content);
                if (!msg.IsSuccessStatusCode)
                {
                    //return StatusCode(Convert.ToInt32(msg.StatusCode));
                    return result;
                }

                string response = await msg.Content.ReadAsStringAsync();
                result.State = 1;
                result.Token = JsonConvert.DeserializeObject<Token>(response);

                //return Content(result, "application/json");
                return result;
            }
        }
    }
}