﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.EventBus
{
    /// <summary>
    /// 事件源接口，所有的事件源都要事先该接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        object EventObject { get; set; }
    }
}
