using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yan.Infrastructure.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;
            if (type.IsGenericType)
            {
                var genrticTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genrticTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string GetGenericTypeName(this object obj)
        {
            return obj.GetType().GetGenericTypeName();
        }
    }
}
