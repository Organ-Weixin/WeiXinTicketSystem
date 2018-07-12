using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum ScoreRecordTypeEnum : byte
    {
        [XmlEnum("1")]
        [Description("用户签到")]
        SignIn = 1
    }
}
