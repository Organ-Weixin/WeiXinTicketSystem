using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum SystemPermissionEnum:byte
    {
        [XmlEnum("R")]
        [Description("查询")]
        Retrieve = 1,

        [XmlEnum("C")]
        [Description("添加")]
        Create = 2,

        [XmlEnum("U")]
        [Description("编缉")]
        Update = 3,

        [XmlEnum("D")]
        [Description("删除")]
        Delete = 4
    }
}
