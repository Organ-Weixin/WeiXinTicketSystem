using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WeiXinTicketSystem.Util
{
    /// <summary>
    /// 提供了一个关于json的辅助类
    /// </summary>
    public static class JSONHelper
    {

        #region Method
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented,
new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool HasNullIgnore)
        {
            if (HasNullIgnore)
                return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!strJson.IsNullOrEmpty())
                return JsonConvert.DeserializeObject<T>(strJson);
            return null;
        }

        /// <summary>
        /// 将xml数据转换为json，再转化为类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xele"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(XElement xele)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xele.ToString());
            string json = JsonConvert.SerializeXmlNode(doc);

            json = json.Replace("@", "");

            var ts = JsonConvert.DeserializeObject<T>(json);
            return ts;
        }
        #endregion

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>      
        public static bool IsNullOrEmpty<T>(this T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()) || data.ToString() == "")
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
    }
}
