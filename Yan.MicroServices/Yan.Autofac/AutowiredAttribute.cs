using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Autofac
{
    /// <summary>
    /// 属性注入 特性
    /// 被该属性 标记的属性 自动进行属性注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutowiredAttribute:Attribute
    {
    }
}
