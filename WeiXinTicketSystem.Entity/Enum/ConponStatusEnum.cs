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
        [Description("非法状态")]
        Illegal = 0,

        [XmlEnum("1")]
        [Description("全部")]
        All = 1,

        [XmlEnum("2")]
        [Description("已使用")]
        Used = 2,

        [XmlEnum("3")]
        [Description("未使用")]
        NotUsed = 3
    }
}
