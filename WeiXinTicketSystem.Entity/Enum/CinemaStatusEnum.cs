using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CinemaStatusEnum:byte
    {
        [XmlEnum("On")]
        On = 1,
        [XmlEnum("Off")]
        Off = 0
    }
}
