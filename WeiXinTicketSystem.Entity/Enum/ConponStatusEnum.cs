using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ConponStatusEnum : byte
    {
        [XmlEnum("0")]
        [Description("全部")]
        All = 0,

        [XmlEnum("1")]
        [Description("已使用")]
        Used = 1,

        [XmlEnum("2")]
        [Description("未使用")]
        NotUsed = 2
    }
}
