using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum UserStampStatusEnum : byte
    {
        [XmlEnum("0")]
        [Description("未使用")]
        NotUse = 0,

        [XmlEnum("1")]
        [Description("已使用")]
        Used = 1
    }
}
