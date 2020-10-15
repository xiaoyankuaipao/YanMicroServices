using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Job
{
    /// <summary>
    /// IJob的“中间” 实现，位于IJobFactory和要运行的IJob之间。
    /// IJobFactor无论请求哪个作业，始终返回QuartzJobRunner的实例
    /// </summary>
    public class QuartzJobRunner : IJob
    {
        private readonly IServiceProvider _provider;

        public QuartzJobRunner(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var jobType = context.JobDetail.JobType;
                var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;
                await job.Execute(context);
            }
        }
    }
}
