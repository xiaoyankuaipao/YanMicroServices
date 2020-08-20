using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yan.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisHelper
    {
        private int DbNum { get; }

        private readonly ConnectionMultiplexer _conn;

        public RedisHelper(string readWriteHosts, int dbNum = 0)
        {
            DbNum = dbNum;
            _conn = RedisConnectionHelp.GetConnectionMultiplexer(readWriteHosts);
        }

        public bool DeleteKey(string key)
        {
            return Do(db => db.KeyDelete(key));
        }

        public bool LockTake(string key, string value, TimeSpan expiry)
        {
            return Do(db => db.LockTake(key, value, expiry));
        }

        public bool LockRelease(string key, string  value)
        {
            return Do(db => db.LockRelease(key, value));
        }

        public bool LockExtend(string key, string value, TimeSpan expiry)
        {
            return Do(db => db.LockExtend(key, value, expiry));
        }
        #region String
        //--------------应用场景---------------
        //String是最常用的一种数据类型，普通的key/value存储都可以归为此类，
        //value其实不仅是String也可以是数字：比如想知道什么时候封锁一个IP地址(访问超过几次)。
        //INCRBY命令让这些变得很容易，通过原子递增保持计数。
        //------------------------------------

        /// <summary>
        /// 存储数据到String
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always)
        {
            return Do(db => db.StringSet(key, value, expiry, when));
        }

        /// <summary>
        /// 获取String类型数据，当可以不存在时返回null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return Do(db => db.StringGet(key, CommandFlags.PreferSlave));
        }

        #endregion

        #region Hash
        //--------------应用场景---------------
        //我们简单举个实例来描述下Hash的应用场景，比如我们要存储一个用户信息对象数据，包含以下信息：  
        //用户ID，为查找的key，  
        //存储的value用户对象包含姓名name，年龄age，生日birthday 等信息，  
        //如果用普通的key/value结构来存储，主要有以下2种存储方式：  
        //第一种方式将用户ID作为查找key,把其他信息封装成一个对象以序列化的方式存储，  
        //如：set u001 "李三,18,20010101"  
        //这种方式的缺点是，增加了序列化/反序列化的开销，并且在需要修改其中一项信息时，需要把整个对象取回，并且修改操作需要对并发进行保护，引入CAS等复杂问题。  
        //第二种方法是这个用户信息对象有多少成员就存成多少个key-value对儿，用用户ID+对应属性的名称作为唯一标识来取得对应属性的值，  
        //如：mset user:001:name "李三",  user:001:age 18, user:001:birthday "20010101"  
        //虽然省去了序列化开销和并发问题，但是用户ID为重复存储，如果存在大量这样的数据，内存浪费还是非常可观的。  
        //那么Redis提供的Hash很好的解决了这个问题，Redis的Hash实际是内部存储的Value为一个HashMap，  
        //并提供了直接存取这个Map成员的接口，  
        //如：hmset user:001 name "李三" age 18 birthday "20010101"     
        //也就是说，Key仍然是用户ID,value是一个Map，这个Map的key是成员的属性名，value是属性值，  
        //这样对数据的修改和存取都可以直接通过其内部Map的Key(Redis里称内部Map的key为field), 也就是通过
        // key(用户ID) + field(属性标签) 操作对应属性数据了，既不需要重复存储数据，也不会带来序列化和并发修改控制的问题。很好的解决了问题。  
        //这里同时需要注意，Redis提供了接口(hgetall)可以直接取到全部的属性数据,但是如果内部Map的成员很多，那么涉及到遍历整个内部Map的操作，
        //由于Redis单线程模型的缘故，这个遍历操作可能会比较耗时，而另其它客户端的请求完全不响应，这点需要格外注意。 
        //-------------------------------------

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return Do(db => db.HashExists(key, dataKey));
        }

        /// <summary>
        /// 存储数据到hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t)
        {
            return Do(db =>
            {
                string json = ConvertJson(t);
                return db.HashSet(key, dataKey, json);
            });
        }

        /// <summary>
        /// 添加数据到hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool HashSet(string key, string dataKey, string data)
        {
            return Do(db =>
            {
                return db.HashSet(key, dataKey, data);
            });
        }

        /// <summary>
        /// 存储多个数据到Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datas"></param>
        public void HashSet(string key, Dictionary<string, string> datas)
        {
            List<HashEntry> hashFields = new List<HashEntry>();

            foreach (var pair in datas.ToArray())
            {
                hashFields.Add(new HashEntry(pair.Key, pair.Value));
            }

            var databse = _conn.GetDatabase(DbNum);
            databse.HashSet(key, hashFields.ToArray());
        }

        /// <summary>
        /// 批量写Hash数据
        /// </summary>
        /// <param name="datas">Key:HashKey,value:DataKey-DataValue</param>
        public void HashBatchSet(Dictionary<string, Dictionary<string, string>> datas)
        {
            var db = GetDatabase();
            var batch = db.CreateBatch();
            foreach (var pair in datas)
            {
                List<HashEntry> hashEntries = new List<HashEntry>();
                foreach (var d in pair.Value)
                {
                    hashEntries.Add(new HashEntry(d.Key, d.Value));
                }
                batch.HashSetAsync(pair.Key, hashEntries.ToArray());
            }
            batch.Execute();
        }

        /// <summary>
        /// 批量写Hash数据
        /// </summary>
        /// <param name="datas"></param>
        public void HashBatchSet(List<KeyValuePair<string, Dictionary<string, string>>> datas)
        {
            var db = GetDatabase();
            var batch = db.CreateBatch();
            foreach (var pair in datas)
            {
                List<HashEntry> hashEntries = new List<HashEntry>();
                foreach (var d in pair.Value)
                {
                    hashEntries.Add(new HashEntry(d.Key, d.Value));
                }
                batch.HashSetAsync(pair.Key, hashEntries.ToArray());
            }
            batch.Execute();
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            return Do(db => db.HashDelete(key, dataKey));
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HasDelete(string key, List<string> dataKeys)
        {
            List<RedisValue> deleteKeys = new List<RedisValue>();
            foreach (var datakey in dataKeys)
            {
                deleteKeys.Add(datakey);
            }

            return Do(db => db.HashDelete(key, deleteKeys.ToArray()));
        }

        /// <summary>
        /// 从hash表中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            return Do(db =>
            {
                var value = db.HashGet(key, dataKey, CommandFlags.PreferSlave);
                return ConvertObj<T>(value);
            });
        }
        /// <summary>
        /// 从hash表中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string HashGet(string key, string dataKey)
        {
            return Do(db =>
            {
                var value = db.HashGet(key, dataKey, CommandFlags.PreferSlave);
                return value.HasValue ? value.ToString() : null;
            });
        }

        /// <summary>
        /// 根据key获取若干个datakey的值，返回顺序按照datakey的顺序，如果datakey不存在，则为null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datakeys"></param>
        /// <returns></returns>
        public string[] HashGetSome(string key, List<string> datakeys)
        {
            return Do(db =>
            {
                RedisValue[] hashFields = Array.ConvertAll(datakeys.ToArray(), x => (RedisValue)x);
                var value = db.HashGet(key, hashFields, CommandFlags.PreferSlave);
                return Array.ConvertAll(value, x => (string)x);
            });
        }

        /// <summary>
        /// 从hash表中，获取key值对应的所有属性和属性值（hashmap）
        /// </summary>
        /// <param name="key"></param>
        public Dictionary<string, string> HashGetALL(string key)
        {
            Dictionary<string, string> dic = null;
            return Do(db =>
            {
                var entries = db.HashGetAll(key, CommandFlags.PreferSlave);
                if (entries != null && entries.Length > 0)
                {
                    dic = new Dictionary<string, string>();
                    foreach (var entry in entries)
                    {
                        dic.Add(entry.Name, entry.Value);
                    }
                }
                return dic;
            });

        }

        /// <summary>
        /// 根据keys批量获取Hash值
        /// 若果某个key不存在，则在最后的结果中不包含该key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> HashBatchGetALL(List<string> keys)
        {
            List<Dictionary<string, string>> lstDatas = new List<Dictionary<string, string>>();
            var db = GetDatabase();
            var batch = db.CreateBatch();
            List<Task<HashEntry[]>> tasks = new List<Task<HashEntry[]>>();

            keys.ForEach(t =>
            {
                tasks.Add(batch.HashGetAllAsync(t, CommandFlags.PreferSlave));
            });

            batch.Execute();
            Task.WaitAll(tasks.ToArray());

            foreach (var t in tasks)
            {
                var data = t.Result;
                if (data == null || data.Length == 0)
                {
                    continue;
                }
                Dictionary<string, string> dics = new Dictionary<string, string>();
                foreach (var d in data)
                {
                    dics.Add(d.Name, d.Value);
                }
                lstDatas.Add(dics);
            }

            return lstDatas;
        }

        /// <summary>
        /// 批量获取Hash（返回指定的datakeys的dataValue，如果某个datakey不存在，则dataValue为null
        /// 若果某个key不存在，则在最后的结果中也包含该key
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="datakeys"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> HashBatchGetSome(List<string> keys, List<string> datakeys)
        {
            List<Dictionary<string, string>> lstDatas = new List<Dictionary<string, string>>();
            var db = GetDatabase();
            var batch = db.CreateBatch();
            List<Task<RedisValue[]>> tasks = new List<Task<RedisValue[]>>();

            RedisValue[] datakeysArr = Array.ConvertAll(datakeys.ToArray(), x => (RedisValue)x);
            keys.ForEach(t =>
            {
                tasks.Add(batch.HashGetAsync(t, datakeysArr, CommandFlags.PreferSlave));
            });

            batch.Execute();
            Task.WaitAll(tasks.ToArray());

            foreach (var t in tasks)
            {
                var data = t.Result;
                if (data == null || data.Length == 0)
                {
                    continue;
                }
                Dictionary<string, string> dics = new Dictionary<string, string>();
                for (int i = 0; i < datakeys.Count; i++)
                {
                    dics.Add(datakeys[i], (string)data[i]);
                }
                lstDatas.Add(dics);
            }

            return lstDatas;
        }

        /// <summary>
        /// 为数字增长1
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            return Do(db => db.HashIncrement(key, dataKey, val));
        }

        /// <summary>
        /// 为hash 的 特定字段增长1
        /// </summary>
        /// <param name="datas"></param>
        public void HashBatchIncrement(List<string> keys, string datatkey)
        {
            var db = GetDatabase();
            var batch = db.CreateBatch();
            foreach (var key in keys)
            {
                batch.HashIncrementAsync(key, datatkey);
            }
            batch.Execute();
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            return Do(db => db.HashDecrement(key, dataKey, val));
        }

        /// <summary>
        /// 获取hashkey所有dataKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            return Do(db =>
            {
                List<string> keys = new List<string>();
                RedisValue[] values = db.HashKeys(key);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        keys.Add(value.ToString());
                    }
                }
                return keys;
            });
        }

        #endregion

        #region keys
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<string> GetKeys(string pattern)
        {
            var keys = Do(db => db.ScriptEvaluate(LuaScript.Prepare(
                 //Redis的keys模糊查询：
                 " local res = redis.call('KEYS', @keypattern) " +
                 " return res "), new { @keypattern = pattern }));

            var lst = ((string[])keys).ToList();
            return lst;

            //var lstKeys = new List<string>();
            //var _server = _conn.GetServer(_conn.GetEndPoints()[0]);
            //var keys = _server.Keys(DbNum, pattern);
            //lstKeys = keys.Select(c => c.ToString()).ToList();
            //return lstKeys;
        }

        #endregion

        #region 辅助方法
        private T Do<T>(Func<IDatabase, T> func)
        {
            var databse = _conn.GetDatabase(DbNum);
            return func(databse);
        }

        private string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }

        private T ConvertObj<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        private IDatabase GetDatabase()
        {
            return _conn.GetDatabase(DbNum);
        }
        #endregion

    }
}

