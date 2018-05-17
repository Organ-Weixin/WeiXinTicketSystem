using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum DefaultActionTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("回复图文")]
        Graphic = 1,

        [XmlEnum("2")]
        [Description("跳转链接")]
        Link = 2,

        [XmlEnum("3")]
        [Description("转到客服")]
        CustomerService = 3,

        [XmlEnum("4")]
        [Description("第三方融合")]
        ThirdParty = 4
    }
}
