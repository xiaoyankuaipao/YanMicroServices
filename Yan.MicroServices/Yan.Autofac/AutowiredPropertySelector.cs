using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac.Core;

namespace Yan.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    public class AutowiredPropertySelector:IPropertySelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(AutowiredAttribute));
        }
    }
}
