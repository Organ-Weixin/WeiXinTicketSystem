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
        [Description("优惠券")]
        Coupon = 1,

        [XmlEnum("2")]
        [Description("通兑券")]
        Exchange = 2,

        [XmlEnum("2")]
        [Description("实物赠品")]
        Gift = 3
    }
}
