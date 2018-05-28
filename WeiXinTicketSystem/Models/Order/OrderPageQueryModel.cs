using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Models
{
    public class OrderPageQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatusEnum? OrderStatus { get; set; }

        /// <summary>
        /// 时间范围
        /// </summary>
        public string OrderDateRange { get; set; }
    }
}