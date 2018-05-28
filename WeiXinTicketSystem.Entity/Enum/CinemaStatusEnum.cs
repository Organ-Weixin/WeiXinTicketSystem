using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CinemaStatusEnum:byte
    {
        [XmlEnum("On")]
        [Description("开通")]
        On = 1,
        [XmlEnum("Off")]
        [Description("关闭")]
        Off = 0
    }
}
