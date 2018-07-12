using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum SessionSeatStatusEnum : byte
    {
        /// <summary>
        /// 非法座位状态
        /// </summary>
        Illegal = 0,

        /// <summary>
        /// 所有
        /// </summary>
        All = 1,

        /// <summary>
        /// 可售出
        /// </summary>
        Available = 2,

        /// <summary>
        /// 已锁定
        /// </summary>
        Locked = 3,

        /// <summary>
        /// 已售出
        /// </summary>
        Sold = 4,

        /// <summary>
        /// 已预订
        /// </summary>
        Booked = 5,

        /// <summary>
        /// 不可用
        /// </summary>
        Unavailable = 6
    }
}
