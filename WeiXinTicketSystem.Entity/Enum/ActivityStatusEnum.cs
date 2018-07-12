using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ActivityStatusEnum:byte
    {
        [XmlEnum("0")]
        [Description("非法状态")]
        Illegal = 0,

        [XmlEnum("1")]
        [Description("全部")]
        All = 1,

        [XmlEnum("2")]
        [Description("已取消")]
        Off = 2,

        [XmlEnum("3")]
        [Description("进行中")]
        On = 3
    }
}
