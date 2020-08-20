using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yan.Utility
{
    /// <summary>
    /// 一致性哈希
    /// </summary>
    public class ConsistentHash
    {
        /// <summary>
        /// 
        /// </summary>
        private SortedDictionary<ulong, string> _circle;

        /// <summary>
        /// 
        /// </summary>
        public ConsistentHash()
        {
            _circle = new SortedDictionary<ulong, string>();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="repeat">该节点下的虚拟节点数</param>
        public  void AddNode(string node, int repeat)
        {
            for (int i = 0; i < repeat; i++)
            {
                string identifier = node.GetHashCode().ToString() + "-" + i;
                ulong hashCode = Md5Hash(identifier);
                _circle.Add(hashCode, node);
            }
        }

        /// <summary>
        /// 根据key值得 获取目标节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  string GetTargetNode(string key)
        {
            ulong hash = Md5Hash(key);
            ulong firstNode = ModifiedBinarySearch(_circle.Keys.ToArray(), hash);
            return _circle[firstNode];
        }

        /// <summary>
        /// 计算hashCode
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  ulong Md5Hash(string key)
        {
            using (var hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                var a = BitConverter.ToUInt64(data, 0);
                var b = BitConverter.ToUInt64(data, 8);
                ulong hashCode = a ^ b;
                return hashCode;
            }
        }

        /// <summary>
        /// 计算key的数值，得出空间归属
        /// </summary>
        /// <param name="sortedArray"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public  ulong ModifiedBinarySearch(ulong[] sortedArray, ulong val)
        {
            int min = 0;
            int max = sortedArray.Length - 1;
            if (val < sortedArray[min] || val > sortedArray[max])
                return sortedArray[0];

            while (max - min > 1)
            {
                int mid = (max + min) / 2;
                if (sortedArray[mid] >= val)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return sortedArray[max];
        }
    }
}
