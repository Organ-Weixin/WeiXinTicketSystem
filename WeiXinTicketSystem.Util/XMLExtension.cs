using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Util
{
    /// <summary>
    /// XML扩展
    /// </summary>
    public static class XMLExtension
    {
        public static string ElementValue(this XElement element, string name, string defaultTo = null)
        {
            var destElement = element.Element(name);
            return destElement != null ? destElement.Value : defaultTo;
        }

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new Utf8StringWriter();

                //空Namespace
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value, ns);
                    return stringWriter.ToString();
                }
            }
            catch (Exception e)
            {
                return "xml序列化异常！异常信息:" + e.Message;
            }
        }

        /// <summary>
        /// 反序列化XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml)
        {
            if (xml == null)
            {
                return default(T);
            }

            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringReader = new StringReader(xml);
                using (var reader = XmlReader.Create(stringReader))
                {
                    return (T)xmlserializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return default(T);
            }
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public Utf8StringWriter()
        {

        }
        public Utf8StringWriter(StringBuilder sb) : base(sb)
        {

        }
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
