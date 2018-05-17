using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum OrderStatusEnum:byte
    {
        [Description("新建")]
        Created = 1,

        [Description("已锁座")]
        Locked = 2,

        [Description("锁座失败")]
        LockFail = 3,

        [Description("已解锁")]
        Released = 4,

        [Description("解锁失败")]
        ReleaseFail = 5,

        /// <summary>
        /// 满天星调用SellTicket接口成功后进入此状态，待轮询确认成功后进入Complete
        /// </summary>
        [Description("已提交")]
        Submited = 6,

        [Description("提交失败")]
        SubmitFail = 7,

        [Description("完成")]
        Complete = 8,

        [Description("已退票")]
        Refund = 9
    }
}
