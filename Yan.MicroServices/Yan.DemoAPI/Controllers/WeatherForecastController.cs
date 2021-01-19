using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yan.Job;

namespace Yan.DemoAPI.Controllers
{
   // [Authorize]
    //[Authorize(Policy = "WeatherPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger/*,IOptions<List<JobSchedule>> info*/)
        {
            _logger = logger;
        }

        [HttpGet]
        public  Task Get()
        {
            return Task.CompletedTask;
        }
    }
}
