using System.ComponentModel;
using System.Xml.Serialization;


namespace WeiXinTicketSystem.Entity.Enum
{
    public enum CustomMenuTypeEnum:byte
    {
        [XmlEnum("1")]
        [Description("Click：点击推事件")]
        Click = 1,

        [XmlEnum("2")]
        [Description("View：跳转URL")]
        View = 2,

        [XmlEnum("3")]
        [Description("Parent: 有下级菜单")]
        Parent = 3,

        [XmlEnum("4")]
        [Description("None：没有设置动作")]
        None = 4,

        [XmlEnum("5")]
        [Description("Default： 默认动作")]
        Default = 5
    }
}
