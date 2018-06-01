using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 套餐状态枚举类
    /// </summary>
    public enum SnackStatusEnum:byte
    {
        [XmlEnum("1")]
        [Description("全部")]
        All=2,

        [XmlEnum("2")]
        [Description("下架")]
        Off = 0,

        [XmlEnum("3")]
        [Description("上架")]
        On = 1
    }
}
