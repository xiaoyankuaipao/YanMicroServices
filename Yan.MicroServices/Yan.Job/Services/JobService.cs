using Quartz;
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
    public class JobService : IJobService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerFactory"></param>
        public JobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <returns></returns>
        public async Task<bool> PauseJobAsync(string jobIdentity, CancellationToken cancellationToken = default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            var job = await scheduler.GetJobDetail(new JobKey(jobIdentity));
            if (job != null)
            {
                await scheduler.PauseJob(new JobKey(jobIdentity));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ResumeJobAsync(string jobIdentity, CancellationToken cancellationToken = default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            var job = await scheduler.GetJobDetail(new JobKey(jobIdentity));
            if (job != null)
            {
                await scheduler.ResumeJob(new JobKey(jobIdentity));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<JobState> GetJobStateAsync(string jobIdentity, CancellationToken cancellationToken = default)
        {
            JobState jobState = new JobState();

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            var job = await scheduler.GetJobDetail(new JobKey(jobIdentity));
            if (job == null)
            {
                return jobState;
            }
            jobState.Identity = job.Key.Name;
            jobState.Description = job.Description;
            jobState.JobType = job.JobType;
            jobState.TriggerStates = new List<TriggerState>();

            var triggers = await scheduler.GetTriggersOfJob(new JobKey(jobIdentity), cancellationToken);
            foreach (var trigger in triggers)
            {
                var triggerState = new TriggerState();
                triggerState.Identity = trigger.Key.Name;
                triggerState.Description = trigger.Description;
                triggerState.NextFireTime = trigger.GetNextFireTimeUtc().Value;
                jobState.TriggerStates.Add(triggerState);

                var state = await scheduler.GetTriggerState(trigger.Key, cancellationToken);
                triggerState.State = (int)state;
            }

            return jobState;
        }
    
    }
}
