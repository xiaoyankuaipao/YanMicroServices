using System;

namespace Yan.DelayQueue
{
    /// <summary>
    /// 延时队列接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDelayQueue<T>
    {
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="delayTime">延时时长</param>
        /// <param name="action">延时到期，要执行的任务</param>
        void EnQueue(long delayTime, Action<T> action);

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="delayTime">延时时长</param>
        /// <param name="action">延时到期，要执行的任务</param>
        /// <param name="data">任务参数</param>
        void EnQueue(long delayTime, Action<T> action, T data);

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="delayTime">延时时长</param>
        /// <param name="action">延时到期，要执行的任务</param>
        /// <param name="data">任务参数</param>
        /// <param name="identity">任务标识，用于取消任务</param>
        void EnQueue(long delayTime, Action<T> action, T data, long identity);

        /// <summary>
        /// 出队，即取消延时任务
        /// </summary>
        /// <param name="identity"></param>
        void Dequeue(long identity);
    }
}
