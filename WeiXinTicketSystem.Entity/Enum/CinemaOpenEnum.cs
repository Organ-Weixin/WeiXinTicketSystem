using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 影院是否开通枚举类
    /// </summary>
    public enum CinemaOpenEnum:byte
    {
        [XmlEnum("1")]
        [Description("开通")]
        Open = 1,

        [XmlEnum("2")]
        [Description("关闭")]
        Closed = 2
    }
}
