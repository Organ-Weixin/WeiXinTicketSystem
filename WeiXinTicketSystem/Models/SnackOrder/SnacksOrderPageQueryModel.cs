using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Models
{
    public class SnacksOrderPageQueryModel: DynatablePageQueryModel
    {

        /// <summary>
        /// 订单状态
        /// </summary>
        public SnackOrderStatusEnum? OrderStatus { get; set; }

        /// <summary>
        /// 时间范围
        /// </summary>
        public string OrderDateRange { get; set; }
    }
}