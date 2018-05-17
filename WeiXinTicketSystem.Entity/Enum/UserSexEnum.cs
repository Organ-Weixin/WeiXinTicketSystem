using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum UserSexEnum:byte
    {
        [Description("男")]
        Male = 1,

        [Description("女")]
        Female = 2
    }
}
