using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Yan.DelayQueue
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DelayQueue<T>:IDelayQueue<T>
    {
        /// <summary>
        /// 数组长度
        /// </summary>
        private readonly int _arrayLength;

        /// <summary>
        /// 表示现在指向环形队列的位置
        /// </summary>
        private long _pointer;

        /// <summary>
        /// 用一个数组表示一个环形的队列，
        /// </summary>
        private ConcurrentBag<DelayContext<T>>[] _arrayQueue;

        /// <summary>
        /// 
        /// </summary>
        private ConcurrentBag<DelayContext<T>>[] ArrayQueue =>
            _arrayQueue ?? (_arrayQueue = new ConcurrentBag<DelayContext<T>>[_arrayLength]);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayLength"></param>
        protected DelayQueue(int arrayLength)
        {
            _arrayLength = arrayLength;

            var timer = new Timer(1000);
            timer.Elapsed += Run;
            timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="action"></param>
        public void EnQueue(long delayTime, Action<T> action)
        {
            EnQueue(delayTime, action, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="action"></param>
        /// <param name="data"></param>
        public void EnQueue(long delayTime, Action<T> action, T data)
        {
            EnQueue(delayTime, action, data, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <param name="identity"></param>
        public void EnQueue(long delayTime, Action<T> action, T data, long identity)
        {
            var (cycle, index) = GetPosition(delayTime);
            ArrayQueue[index] = ArrayQueue[index] ?? (ArrayQueue[index] = new ConcurrentBag<DelayContext<T>>());
            var context = new DelayContext<T>(cycle, action, data, identity);
            ArrayQueue[index].Add(context);
            Console.WriteLine("add a delay task,it will be executed after " + delayTime + " seconds");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        public void Dequeue(long identity)
        {
            Parallel.ForEach(ArrayQueue, (collection, state) =>
            {
                var result = collection.FirstOrDefault(c => c.Identity == identity);
                if (result != null)
                {
                    collection.TryTake(out _);
                    state.Break();
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run(object sender, System.Timers.ElapsedEventArgs e)
        {   
            Move();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 
        /// </summary>
        private void Move()
        {
            var currentPointer = GetPointer();
            if (currentPointer >= _arrayLength - 1)
            {
                Interlocked.Exchange(ref _pointer, 0);
            }
            else
            {
                Interlocked.Increment(ref _pointer);
            }

            currentPointer = GetPointer();
            var collections = ArrayQueue[currentPointer];
            if (collections != null && collections.Count > 0)
            {
                Parallel.ForEach(collections, task =>
                {
                    if (task.Cycle == 0)
                    {
                        collections.TryTake(out task);
                        task.ToDoAction(task.Data);
                    }
                    else if (task.Cycle > 0)
                    {
                        task.DeCycle();
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        private (long cycle, long index) GetPosition(long delayTime)
        {
            long currentPointer = GetPointer();
            long cycle;
            long index;

            if (delayTime <= _arrayLength)
            {
                cycle = 0;
                index = (currentPointer + delayTime) < _arrayLength
                    ? (currentPointer + delayTime)
                    : (currentPointer + delayTime - _arrayLength);
            }
            else
            {
                cycle = delayTime / _arrayLength;
                var s1 = delayTime % _arrayLength;

                if (s1 == 0)
                {
                    cycle -= 1;
                }

                index = (s1 + currentPointer) < _arrayLength
                    ? (s1 + currentPointer)
                    : (s1 + currentPointer - _arrayLength);
            }

            return (cycle, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private long GetPointer()
        {
            return Interlocked.Read(ref _pointer);
        }
    }
}
