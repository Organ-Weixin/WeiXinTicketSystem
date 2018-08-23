using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ActivityPopupEnum : byte
    {
        [XmlEnum("1")]
        [Description("领券弹窗")]
        VoucherWindow = 1,

        [XmlEnum("2")]
        [Description("充值/注册")]
        RechargeOrRegister = 2,

        [XmlEnum("3")]
        [Description("抢票")]
        TicketRobbing = 3,

        [XmlEnum("4")]
        [Description("大转盘")]
        SlyderAdventures = 4,

        [XmlEnum("5")]
        [Description("签到")]
        SignIn = 5
    }
}
