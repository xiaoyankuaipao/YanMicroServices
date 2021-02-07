using System;

namespace Yan.Admin.Modules
{
    /// <summary>
    /// 属性注入 特性
    /// 被该特性 标记的属性，进行自动的属性注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutowiredAttribute:Attribute
    {
    }
}
