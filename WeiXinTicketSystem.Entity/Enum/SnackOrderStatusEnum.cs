using System.ComponentModel;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum SnackOrderStatusEnum:byte
    {
        [Description("新建")]
        Created = 1,

        [Description("已支付")]
        Pay = 2,

        [Description("完成")]
        Complete = 3,

        [Description("已取货")]
        Fetched = 4
    }
}
