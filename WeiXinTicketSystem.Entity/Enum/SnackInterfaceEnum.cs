using System.ComponentModel;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 套餐接口枚举类
    /// </summary>
    public enum SnackInterfaceEnum: byte
    {
        [Description("开通")]
        Open = 1,

        [Description("关闭")]
        Close = 0
    }
}
