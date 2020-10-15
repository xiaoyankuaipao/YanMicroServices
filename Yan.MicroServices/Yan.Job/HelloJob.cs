using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Job
{
    [DisallowConcurrentExecution]//防止Quartz.Net同时运行同一个作业
    public class HelloJob : IBaseJob
    {
        private readonly ILogger<HelloJob> _logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            _logger = logger;
        }

        public static string Cors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("hello at {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return Task.CompletedTask;
        }
    }
}
