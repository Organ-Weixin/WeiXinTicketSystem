using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum GiftStatusEnum:byte
    {
        [XmlEnum("0")]
        [Description("非法状态")]
        Illegal = 0,

        [XmlEnum("1")]
        [Description("全部")]
        All = 1,

        [XmlEnum("2")]
        [Description("下架")]
        Off = 2,

        [XmlEnum("3")]
        [Description("上架")]
        On = 3
    }
}
