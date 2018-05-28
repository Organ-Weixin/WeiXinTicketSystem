using System.ComponentModel;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 情侣座标识
    /// </summary>
    public enum LoveFlagEnum
    {
        /// <summary>
        /// 普通座
        /// </summary>
        [Description("N")]
        Normal = 0,
        /// <summary>
        /// 情侣座左边座位
        /// </summary>
        [Description("L")]
        LEFT = 1,
        /// <summary>
        /// 情侣座右边座位
        /// </summary>
        [Description("R")]
        RIGHT = 2
    }
}
