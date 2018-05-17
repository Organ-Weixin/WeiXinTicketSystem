using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CinemaFeeGatherTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("按每张票收取")]
        Ticket = 1,

        [XmlEnum("2")]
        [Description("按下单次数收取")]
        Order = 2
    }
}
