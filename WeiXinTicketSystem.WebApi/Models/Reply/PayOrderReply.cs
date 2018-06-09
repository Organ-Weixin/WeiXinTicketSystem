using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PayOrderReply: BaseReply
    {
        public PayOrderReplyOrder data { get; set; }
    }
    public class PayOrderReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public bool OrderPayFlag { get; set; }
        public string OrderTradeNo { get; set; }
        public DateTime OrderPayTime { get; set; }
    }
}