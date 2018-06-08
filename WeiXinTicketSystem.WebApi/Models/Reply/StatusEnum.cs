using System.ComponentModel;

namespace WeiXinTicketSystem.WebApi.Models
{
    /// <summary>
    /// 返回状态枚举
    /// </summary>
    public enum StatusEnum : byte
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("Failure")]
        Failure = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("Success")]
        Success = 1
    }
}
