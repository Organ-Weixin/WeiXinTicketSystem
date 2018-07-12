using System.Xml.Serialization;
using System.ComponentModel;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum FilmStatusEnum:byte
    {
        /// <summary>
        /// NoPassed
        /// </summary>
        [XmlEnum("NoPassed")]
        [Description("待审核")]
        NoPassed = 0,

        /// <summary>
        /// Passed
        /// </summary>
        [XmlEnum("Passed")]
        [Description("通过")]
        Passed = 1
    }
}
