using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum YesOrNoEnum : byte
    {
        /// <summary>
        /// No
        /// </summary>
        [XmlEnum("No")]
        No = 0,

        /// <summary>
        /// Yes
        /// </summary>
        [XmlEnum("Yes")]
        Yes = 1
    }
}
