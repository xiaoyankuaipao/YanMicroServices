using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yan.Job
{
    /// <summary>
    /// 
    /// </summary>
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        /// <summary>
        /// 
        /// </summary>
        public IScheduler Scheduler { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerFactory"></param>
        /// <param name="jobFactory"></param>
        /// <param name="jobSchedules"></param>
        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            _jobFactory = jobFactory ?? throw new ArgumentNullException(nameof(jobFactory));
            _jobSchedules = jobSchedules ?? throw new ArgumentNullException(nameof(jobSchedules));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var triggers = CreateTriggers(jobSchedule);
                await Scheduler.ScheduleJob(job, triggers, true,cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobSchedule"></param>
        /// <returns></returns>
        private  IJobDetail CreateJob(JobSchedule jobSchedule)
        {
            var jobType = jobSchedule.JobType;
            return JobBuilder.Create(jobType)
                .WithIdentity(jobSchedule.Identity)
                .WithDescription(jobSchedule.Description)
                .Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobSchedule"></param>
        /// <returns></returns>
        private List<ITrigger> CreateTriggers(JobSchedule jobSchedule)
        {
            List<ITrigger> triggers = new List<ITrigger>();
            for (var i = 0; i < jobSchedule.TriggerInfos.Count; i++)
            {
                var triggerInfo = jobSchedule.TriggerInfos[i];
                var trigger = TriggerBuilder.Create()
                 .WithIdentity(triggerInfo.Identity)
                 .WithCronSchedule(triggerInfo.CronExpression)
                 .WithDescription(triggerInfo.Description)
                 .Build();
                triggers.Add(trigger);
            }

            return triggers;
        }

    }
}
