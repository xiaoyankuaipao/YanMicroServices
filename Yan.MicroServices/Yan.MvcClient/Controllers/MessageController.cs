using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yan.Core.Dtos;
using Yan.MvcClient.Clients;

namespace Yan.MvcClient.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ArticleServiceClient _articleClient;

        /// <summary>
        /// 
        /// </summary>
        public MessageController(ArticleServiceClient articleClient)
        {
            _articleClient = articleClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<Clients.PageResultDto<MessageOutputDto>> GetMessagePage(int page, int size)
        {
            var result = await _articleClient.GetMessagePage(page, size);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HandleResultDto> AddMessage(string message)
        {
            MessageCreateDto input = new MessageCreateDto() { Message = message };

            if (User.Identity.IsAuthenticated)
            {
                input.UserName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            }
            var result = await _articleClient.AddMessage(new MessageCreateDto { Message = message });
            return result;
        }

    }
}