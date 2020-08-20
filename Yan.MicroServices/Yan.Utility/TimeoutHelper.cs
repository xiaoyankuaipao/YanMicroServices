using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yan.Utility
{
    /// <summary>
    /// 超时方法帮助类
    /// </summary>
    public class TimeoutHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private ManualResetEvent timeoutEvent;

        /// <summary>
        /// 是否超时
        /// </summary>
        private bool isTimeout;

        /// <summary>
        /// 方法返回
        /// </summary>
        public object _Result = null;

        /// <summary>
        /// 方法委托
        /// </summary>
        public Func<object, object> DoSomething;

        /// <summary>
        /// 
        /// </summary>
        private CancellationTokenSource source;

        /// <summary>
        /// 
        /// </summary>
        public TimeoutHelper()
        {
            this.timeoutEvent = new ManualResetEvent(true);
            source = new CancellationTokenSource();
        }
        ///<summary>
        /// 设定超时时间 异步执行某个方法
        ///</summary>
        ///<returns></returns>
        public bool ExecuteMethodWithTimeout(TimeSpan timeSpan, object @object)
        {
            if (this.DoSomething == null)
            {
                return false;
            }
            this.timeoutEvent.Reset();
            this.isTimeout = true;

            Task task = Task.Run(() =>
            {
                try
                {
                    _Result = DoSomething.Invoke(@object);
                    this.isTimeout = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    this.isTimeout = true;
                }
                finally
                {
                    this.timeoutEvent.Set();
                }

            }, source.Token);

            //在指定时间内收到信息信号，返回ture
            if (this.timeoutEvent.WaitOne(timeSpan, false))
            {
                this.isTimeout = false;
            }
            else
            {
                source.Cancel();
            }
            return this.isTimeout;
        }
    }

    #region 使用方式实例
    //    static void Main(string[] args)
    //    {
    //        TimeoutHelper timeout = new TimeoutHelper();

    //        AgentInfo info = new AgentInfo
    //        {
    //            Uri = "http://10.100.45.100:8500",
    //            ServerName = "ParkingServices"
    //        };

    //        timeout.DoSomething = GetServer;

    //        bool isTimeOut = timeout.ExecuteMethodWithTimeout(new TimeSpan(0, 0, 5), info);

    //        if (isTimeOut)
    //        {
    //            Console.WriteLine("操作超时,该干嘛就干嘛去吧");
    //        }
    //        else
    //        {
    //            Console.WriteLine("操作没有超时,返回=" + timeout._Result);
    //        }

    //        Console.ReadKey();
    //    }



    //    private static object GetServer(object info)
    //    {
    //        Thread.Sleep(10000);

    //        AgentService server = null;
    //        using (var consul = new ConsulClient(c =>
    //        {
    //            c.Address = new Uri(((AgentInfo)info).Uri);
    //        }))
    //        {
    //            try
    //            {
    //                var result = consul.Agent.Services().Result;

    //                var services = result.Response.Values.Where(p =>
    //                   p.Service.Equals(((AgentInfo)info).ServerName, StringComparison.OrdinalIgnoreCase));

    //                Random rand = new Random();
    //                var index = rand.Next(services.Count());
    //                server = services.ElementAt(index);
    //                return server;
    //            }
    //            catch (Exception ex)
    //            {
    //                return 456;
    //            }

    //        }
    //    }


    //}

    //public class AgentInfo
    //{
    //    public string Uri { get; set; }

    //    public string ServerName { get; set; }
    //}
    #endregion
}
