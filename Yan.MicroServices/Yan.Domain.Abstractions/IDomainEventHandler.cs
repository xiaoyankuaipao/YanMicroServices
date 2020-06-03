using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Domain.Abstractions
{
    /// <summary>
    /// 领域事件处理器接口
    /// 这里需要约束泛型的TDomainEvent必须是一个IDomainEvent
    /// 也就是我们的处理程序只处理IDomianEvent作为入参的事件
    /// INotificationHandler接口是MediatR组件提供的接口
    /// 这里我们使用INotificationHandler的Handler来作为处理方法的定义
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<TDomainEvent>:INotificationHandler<TDomainEvent>
        where TDomainEvent:IDomainEvent
    {
    }
}
