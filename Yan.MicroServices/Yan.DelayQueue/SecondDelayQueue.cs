namespace Yan.DelayQueue
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SecondDelayQueue<T> : DelayQueue<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayLength"></param>
        public SecondDelayQueue(int arrayLength) : base(arrayLength)
        {
        }
    }
}
