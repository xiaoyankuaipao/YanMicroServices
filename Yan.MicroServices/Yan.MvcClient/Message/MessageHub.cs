using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.MvcClient.Message
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub:Hub
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public MessageHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user != null)
            {
                var test = user.Claims.ToList();
                var userName = user.Claims.FirstOrDefault(c => c.Type == "realname")?.Value;
                if (!String.IsNullOrEmpty(userName))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, userName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user != null)
            {
                var userName = user.Claims.FirstOrDefault(c => c.Type == "realname")?.Value;
                if (!String.IsNullOrEmpty(userName))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, userName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("AddNewMessage", user, message);
        }
    }
}
