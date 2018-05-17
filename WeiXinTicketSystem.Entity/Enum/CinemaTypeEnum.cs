using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 影院类型枚举
    /// </summary>
    public enum CinemaTypeEnum : byte
    {
        [XmlEnum("1")]
        [Description("13规范")]
        NationalStandard = 1,

        [XmlEnum("2")]
        [Description("辰星")]
        ChenXing = 2,

        [XmlEnum("4")]
        [Description("鼎新")]
        DingXin = 4,

        [XmlEnum("8")]
        [Description("满天星")]
        ManTianXing = 8,

        [XmlEnum("16")]
        [Description("火烈鸟")]
        HuoLieNiao = 16,

        [XmlEnum("32")]
        [Description("电影1905")]
        DianYing1905 = 32,

        [XmlEnum("64")]
        [Description("粤科")]
        YueKe = 64
    }
}
