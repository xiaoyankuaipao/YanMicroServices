using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Yan.Job;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加作业服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jobSchedules"></param>
        /// <returns></returns>
        public static IServiceCollection AddJob(this IServiceCollection services, List<JobSchedule> jobSchedules)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<QuartzJobRunner>();
            services.AddSomeJob(jobSchedules);

            services.AddHostedService<QuartzHostedService>();

            services.AddScoped<IJobService, JobService>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jobSchedules"></param>
        /// <returns></returns>
        private static IServiceCollection AddSomeJob(this IServiceCollection services, List<JobSchedule> jobSchedules)
        {
            foreach(var job in jobSchedules)
            {
                services.AddScoped(job.JobType);
                services.AddSingleton(job);
            }

            return services;
        }

    }
}
