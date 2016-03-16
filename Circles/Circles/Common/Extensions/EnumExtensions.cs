using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Circles.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string ToEnumString<T>(this T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute =
                ((EnumMemberAttribute[])enumType.GetRuntimeField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true));

            return enumMemberAttribute.Length > 0 ? enumMemberAttribute.Single().Value : name;
        }
    }
}
