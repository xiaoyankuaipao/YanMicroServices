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
    public class JobService
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
        public async Task PauseJob(string jobIdentity, CancellationToken cancellationToken=default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            var job = await scheduler.GetJobDetail(new JobKey(jobIdentity));
            if (job != null)
            {
                await scheduler.PauseJob(new JobKey(jobIdentity));
            }
        }

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ResumeJob(string jobIdentity, CancellationToken cancellationToken = default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            var job = await scheduler.GetJobDetail(new JobKey(jobIdentity));
            if (job != null)
            {
                await scheduler.ResumeJob(new JobKey(jobIdentity));
            }
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task GetTriggerState(string jobIdentity, CancellationToken cancellationToken = default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

        }

    }
}
