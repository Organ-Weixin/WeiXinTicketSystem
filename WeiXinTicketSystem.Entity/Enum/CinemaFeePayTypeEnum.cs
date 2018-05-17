using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CinemaFeePayTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("客人支付")]
        Customer = 1,

        [XmlEnum("2")]
        [Description("影城支付")]
        Cinema = 2
    }
}
