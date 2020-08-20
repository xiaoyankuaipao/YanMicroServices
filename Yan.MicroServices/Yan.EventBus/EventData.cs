using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.EventBus
{
    /// <summary>
    /// 事件源：描述事件信息，用于参数传递
    /// </summary>
    public class EventData : IEventData
    {
        /// <summary>
        /// 时间发生的时间
        /// </summary>
        public DateTime EventTime { get ; set ; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        public Object EventObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}
