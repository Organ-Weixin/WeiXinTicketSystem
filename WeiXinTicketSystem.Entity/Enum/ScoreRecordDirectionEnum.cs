using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ScoreRecordDirectionEnum : byte
    {
        [XmlEnum("0")]
        [Description("获得")]
        Obtain = 0,

        [XmlEnum("1")]
        [Description("支出")]
        Spend = 1
    }
}
