using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum WeChatOfficialAccountTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("服务号")]
        Service = 1,

        [XmlEnum("2")]
        [Description("订阅号")]
        Subscribe = 2
    }
}
