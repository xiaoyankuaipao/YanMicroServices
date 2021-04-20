using System;
using System.Threading;

namespace Yan.DelayQueue
{
    /// <summary>
    /// 延时任务
    /// </summary>
    public class DelayContext<T>:IDelayContext
    {
        /// <summary>
        /// 周期，当_cycle=0时，表示要准备执行了
        /// </summary>
        private long _cycle;

        /// <summary>
        /// 任务
        /// </summary>
        public Action<T> ToDoAction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Cycle
        {
            get => Interlocked.Read(ref _cycle);
            set => Interlocked.Exchange(ref _cycle, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public long Identity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="action"></param>
        public DelayContext(long cycle, Action<T> action)
        {
            _cycle = cycle;
            ToDoAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="action"></param>
        /// <param name="data"></param>
        public DelayContext(long cycle, Action<T> action, T data)
        {
            _cycle = cycle;
            ToDoAction = action;
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <param name="identity"></param>
        public DelayContext(long cycle, Action<T> action, T data, long identity)
        {
            _cycle = cycle;
            ToDoAction = action;
            Data = data;
            Identity = identity;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeCycle()
        {
            Interlocked.Decrement(ref _cycle);
        }

    }
}
