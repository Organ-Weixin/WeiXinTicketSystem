using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CustomMenuReTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("Text: 回复文本")]
        Text = 1,

        [XmlEnum("2")]
        [Description("News: 回复图文消息")]
        News = 2
    }
}
