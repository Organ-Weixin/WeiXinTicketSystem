using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum MemberCardStatusEnum : byte
    {
        [XmlEnum("0")]
        [Description("未绑定")]
        NoBind = 0,

        [XmlEnum("2")]
        [Description("已绑定")]
        Bind = 2
    }
}
