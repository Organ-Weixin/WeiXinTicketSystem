using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum YesOrNoEnum : byte
    {
        /// <summary>
        /// No
        /// </summary>
        [XmlEnum("No")]
        [Description("否")]
        No = 0,

        /// <summary>
        /// Yes
        /// </summary>
        [XmlEnum("Yes")]
        [Description("是")]
        Yes = 1
    }
}
