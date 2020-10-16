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
    public interface IJobService
    {
        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <returns></returns>
        Task<bool> PauseJobAsync(string jobIdentity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ResumeJobAsync(string jobIdentity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="jobIdentity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JobState> GetJobStateAsync(string jobIdentity, CancellationToken cancellationToken = default);


    }
}
