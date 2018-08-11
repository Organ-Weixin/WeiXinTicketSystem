using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ConponTypeEnum : byte
    {
        [XmlEnum("1")]
        [Description("卖品优惠券")]
        SellGoodsCoupon = 1,

        [XmlEnum("2")]
        [Description("电影票优惠券")]
        TicketCoupon = 2,

        [XmlEnum("3")]
        [Description("卖品通兑券")]
        SellGoodsExchange = 3,

        [XmlEnum("4")]
        [Description("电影票通兑券")]
        TicketExchange = 4,

        [XmlEnum("5")]
        [Description("实物赠品")]
        Gift = 5
    }
}
