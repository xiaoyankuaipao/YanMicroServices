using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yan.DemoAPI.MyJob
{
    [DisallowConcurrentExecution] //该属性可防止Quartz.NET尝试同时运行同一作业。
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;

        public HelloWorldJob(ILogger<HelloWorldJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello word by Quartz at {0}", DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss"));

            return Task.CompletedTask;
        }
    }
}
