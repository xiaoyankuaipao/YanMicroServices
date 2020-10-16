using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yan.Job;

namespace Yan.DemoAPI.Controllers
{
   // [Authorize]
    //[Authorize(Policy = "WeatherPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IJobService _jobService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<JobState> Get()
        {

            var state = await _jobService.GetJobStateAsync("job123");

            return state;
        }
    }
}
