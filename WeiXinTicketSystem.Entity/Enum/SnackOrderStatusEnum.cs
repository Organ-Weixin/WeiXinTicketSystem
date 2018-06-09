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

        [Description("已预定")]
        Booked = 2,

        [Description("预定失败")]
        BookFail =3,

        [Description("已支付")]
        Payed = 4,

        [Description("支付失败")]
        PayFail = 5,

        [Description("已取消")]
        Released = 6,

        [Description("取消失败")]
        ReleaseFail = 7,

        [Description("已提交")]
        Submited = 8,

        [Description("提交失败")]
        SubmitFail = 9,

        [Description("完成")]
        Complete = 10,

        [Description("已退单")]
        Refund = 11,

        [Description("退订失败")]
        RefundFail = 12,

        [Description("已取货")]
        Fetched = 13
    }
}
