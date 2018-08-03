using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum PayTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("微信支付")]
        WxPay = 1,

        [XmlEnum("2")]
        [Description("通兑券支付")]
        VoucherPay = 2,

        [XmlEnum("3")]
        [Description("抵金券+微信支付")]
        ConponPay = 3,

        [XmlEnum("4")]
        [Description("会员卡")]
        MemberCardPay = 4
    }
}
