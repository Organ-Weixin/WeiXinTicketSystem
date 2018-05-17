using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum PayTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("支付宝")]
        Alipay = 1,

        [XmlEnum("2")]
        [Description("百度钱包")]
        Bfbpay = 2,

        [XmlEnum("3")]
        [Description("会员卡")]
        MemberCard = 3,

        [XmlEnum("4")]
        [Description("微信支付")]
        Wxpay = 4
    }
}
