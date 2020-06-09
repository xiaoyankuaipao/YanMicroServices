using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Yan.Core.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum)
        {
            if (@enum == null)
            {
                return string.Empty;
            }

            string stringValue = @enum.ToString();

            FieldInfo fieldInfo = @enum.GetType().GetField(stringValue);
            if (fieldInfo == null)
            {
                return @enum.ToString();
            }

            object[] objects = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objects.Length == 0)
            {
                return @enum.ToString();
            }

            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objects[0];
            return descriptionAttribute.Description;
        }
    }
}
