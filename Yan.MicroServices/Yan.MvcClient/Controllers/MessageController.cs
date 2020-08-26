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
        public async Task<MessageCreateDto> AddMessage(CreateMessagDto dto)
        {
            MessageCreateDto input = new MessageCreateDto() { Message = dto.Message };

            if (User.Identity.IsAuthenticated)
            {
                var nameClaim = User.Claims.FirstOrDefault(c => c.Type == "name");
                if (nameClaim == null)
                {
                    input.UserName = "佚名";
                }
                else
                {
                    input.UserName = nameClaim.Value;
                }
            }
            else
            {
                input.UserName = "佚名";
            }
            var result = await _articleClient.AddMessage(input);
            if(result.State==1)
            {
                return input;
            }
            return null;
        }

    }
}