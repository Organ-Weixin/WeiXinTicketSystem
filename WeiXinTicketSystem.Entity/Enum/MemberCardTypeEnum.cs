using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum MemberCardTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("普通会员卡")]
        Normal = 1,

        [XmlEnum("2")]
        [Description("奥斯卡会员卡")]
        Oscar = 2,

        [XmlEnum("3")]
        [Description("第三方会员卡")]
        Thirdparty = 3
    }
}
