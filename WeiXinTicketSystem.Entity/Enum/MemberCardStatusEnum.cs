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
        [Description("启用")]
        Enable = 0,

        [XmlEnum("1")]
        [Description("注销")]
        Cancellation = 1,

        [XmlEnum("2")]
        [Description("已绑定")]
        Bind = 2,
    }
}
