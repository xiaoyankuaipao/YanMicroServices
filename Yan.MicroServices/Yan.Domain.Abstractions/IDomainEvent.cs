using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Domain.Abstractions
{
    /// <summary>
    /// 领域事件接口
    /// 这是一个空接口，同来标记是否是领域事件
    /// INotification是MediatR组件提供的一个空接口，用来实现事件的传递
    /// </summary>
    public interface IDomainEvent:INotification
    {
    }
}
