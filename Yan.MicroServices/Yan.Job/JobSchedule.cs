using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yan.Job
{
    /// <summary>
    /// Job调度中间对象
    /// </summary>
    public class JobSchedule
    {
        /// <summary>
        /// Job标识
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// job类型
        /// </summary>
        public Type JobType { get; private set; }

        /// <summary>
        /// Job 执行策略
        /// </summary>
        public List<TriggerInfo> TriggerInfos { get; private set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jobType"></param>
        /// <param name="cronExpressions"></param>
        /// <param name="description"></param>
        public JobSchedule(string identity, Type jobType, List<TriggerInfo> triggerInfos, string description)
        {
            this.Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            this.JobType = jobType ?? throw new ArgumentNullException(nameof(jobType));
            this.TriggerInfos = triggerInfos ?? throw new ArgumentNullException(nameof(triggerInfos));
            this.Description = description;
        }
    }

    /// <summary>
    /// 触发器信息
    /// </summary>
    public class TriggerInfo
    {
        /// <summary>
        /// 表示
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public string CronExpression { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="cronExpression"></param>
        /// <param name="description"></param>
        public TriggerInfo(string identity, string cronExpression, string description)
        {
            this.Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            this.CronExpression = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));
            this.Description = description;
        }
    }

}
