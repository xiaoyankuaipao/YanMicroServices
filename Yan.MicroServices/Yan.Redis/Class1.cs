using System;

namespace Yan.Redis
{
    public class Class1
    {
        /// <summary>
        /// 使用redis实现分布式锁1
        /// </summary>
        static void DistributedLock()
        {
            RedisHelper redis = new RedisHelper("10.100.45.170:6379,allowadmin=true,password=123456,syncTimeout=5000");
            var lockKey = Guid.NewGuid().ToString();
            var isLocked = false;
            var value = "a";
            do
            {
                //使用do-while先去给Redis的lockKey随便设置一个值            
                //但是设置的条件是，如果当前lockKey存在（表示其它进程已经锁定了）就返回false            
                //如果lockKey不存在（表示当前没有其它进程锁定）,就反回true,并且设置过期时间为600毫秒（如果进行没有释放，报错死锁的情况）
                isLocked = redis.StringSet(lockKey, value, TimeSpan.FromMilliseconds(600), StackExchange.Redis.When.NotExists);

                if (isLocked)
                {
                    //获得锁
                    Console.WriteLine("I have got the Lock,I can do something right now！");
                }
                else
                {
                    //如果isLocked反回false表示被其它进程锁定，那么当前进程休眠200毫秒后，再去设置锁            
                    //重复此动作直到当前进程拿到锁为止
                    System.Threading.Thread.Sleep(200);
                }

            } while (!isLocked);

            redis.DeleteKey(lockKey);
        }

        /// <summary>
        /// 使用redis实现分布式锁2
        /// </summary>
        static void DistributedLock2()
        {
            RedisHelper redis = new RedisHelper("10.100.45.170:6379,allowadmin=true,password=123456,syncTimeout=5000");
            var lockToken = Guid.NewGuid().ToString();
            var isLocked = false;
            int count = 0;
            do
            {
                if (redis.LockTake("name", lockToken, TimeSpan.FromSeconds(30)))
                {
                    isLocked = true;
                    try
                    {
                        Console.WriteLine(" OK <<<<<<<");
                        System.Threading.Thread.Sleep(20000);
                        //在实际应用中，如果业务代码执行时间比锁的有效期长，可以采用这种方式续期。但是真正应用中如何判断业务代码执行时间比锁的有效期长是个问题，需要好好考虑一下。
                        redis.LockExtend("name", lockToken, TimeSpan.FromSeconds(30));
                        Console.WriteLine(" 续期30S ");
                        System.Threading.Thread.Sleep(25000);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        redis.LockRelease("name", lockToken);
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine(" No 》》》》》》" + ++count);
                }
            }
            while (!isLocked);

        }
    }
}
