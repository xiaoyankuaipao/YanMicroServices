using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Domain.Abstractions
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 获取Id
        /// </summary>
        /// <returns></returns>
        object[] GetKeys();
    }

    /// <summary>
    /// 泛型实体接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// 获取Id
        /// </summary>
        TKey Id { get; }
    }
}
