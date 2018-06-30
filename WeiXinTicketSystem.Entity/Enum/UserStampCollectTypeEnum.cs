using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum UserStampCollectTypeEnum : byte
    {
        [XmlEnum("1")]
        [Description("激活码")]
        type1 = 1,

        [XmlEnum("2")]
        [Description("扫一扫")]
        type2 = 2
    }
}
