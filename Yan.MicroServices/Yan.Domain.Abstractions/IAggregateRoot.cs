using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Domain.Abstractions
{
    /// <summary>
    /// 聚合根接口
    /// 就是一个空接口，没有任何方法
    /// 它的作用是在实现仓储层的时候，让一个仓储对应一个聚合根
    /// </summary>
    public interface IAggregateRoot
    {
    }
}
