using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Job
{
    public class JobState
    {
        /// <summary>
        /// Job标识
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// job类型
        /// </summary>
        public Type JobType { get; set; }

        /// <summary>
        /// Job 执行策略
        /// </summary>
        public List<TriggerState> TriggerStates { get;  set; }

        /// <summary>
        /// Job描述
        /// </summary>
        public string Description { get;  set; }
        
    }

    /// <summary>
    /// 触发器状态
    /// </summary>
    public class TriggerState
    {
        /// <summary>
        /// 表示
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// 正常 = 1,暂停 = 2,完成 = 3,错误 = 4,阻塞 = 5,不存在 = 6
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTimeOffset NextFireTime { get; set; }

    }
}
