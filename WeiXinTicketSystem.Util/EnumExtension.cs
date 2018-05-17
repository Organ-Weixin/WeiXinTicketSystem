using System;
using System.ComponentModel;
using System.Reflection;

namespace WeiXinTicketSystem.Util
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述，描述为空返回本身字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }

        public static string GetValueString<T>(this T enumerationValue)
           where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            return Convert.ToInt32(enumerationValue).ToString();
        }

        /// <summary>
        /// 获取数字对应的泛型版本
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TOutput CastToEnum<TOutput>(this short value)
            //where TInput : struct
            where TOutput : struct
        {
            return (TOutput)Enum.ToObject(typeof(TOutput), value);
        }

        /// <summary>
        /// 获取对应名称的枚举
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TOutput CastToEnum<TOutput>(this string name)
            where TOutput : struct
        {
            try
            {
                return (TOutput)Enum.Parse(typeof(TOutput), name, true);
            }
            catch
            {
                return default(TOutput);
            }
        }

        /// <summary>
        /// 获取数字对应枚举的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(this short value)
            where T : struct
        {
            return value.CastToEnum<T>().GetDescription();
        }
    }
}
