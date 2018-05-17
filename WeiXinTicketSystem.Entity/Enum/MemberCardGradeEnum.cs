using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum MemberCardGradeEnum:byte
    {
        [XmlEnum("1")]
        [Description("金卡会员卡")]
        Gold = 1,

        [XmlEnum("2")]
        [Description("银卡会员卡")]
        Silver = 2,

        [XmlEnum("3")]
        [Description("普通会员卡")]
        Normal = 3
    }
}
