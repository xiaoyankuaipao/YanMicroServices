using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Job
{
    /// <summary>
    /// 
    /// </summary>
    public class JobFactory:IJobFactory
    {
        private readonly IServiceProvider _provider;

        public JobFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _provider.GetRequiredService<QuartzJobRunner>();
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
