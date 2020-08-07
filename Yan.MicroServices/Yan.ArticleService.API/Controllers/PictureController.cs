using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yan.Core.Dtos;

namespace Yan.ArticleService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/articlemanage/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IHostingEnvironment hostingEnv;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public PictureController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<HandleResultDto> Post(IFormCollection Files)
        {
            List<string> filePathResultList = new List<string>();

            //string dd = Files["File"];
            //var form = Files;
            //IFormFileCollection cols = Request.Form.Files;
            IFormFileCollection cols = Files.Files;
            foreach (var file in cols)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string filePath = hostingEnv.WebRootPath + $@"/Files/Pictures/";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = fileName.Split('.')[1];
                fileName = Guid.NewGuid() + "." + suffix;
                string fileFullName = filePath + fileName;
                using (var fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                filePathResultList.Add($"/api/articlemanage/src/Pictures/{fileName}");
            }

            HandleResultDto response = new HandleResultDto()
            {
                State = 1,
                Message = JsonConvert.SerializeObject(filePathResultList)
            };

            return response;
        }
    }
}
