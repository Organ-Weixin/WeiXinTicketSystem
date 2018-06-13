using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class RefundSnacksReply: BaseReply
    {
        public RefundSnacksReplyOrder data { get; set; }
    }
    public class RefundSnacksReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public DateTime RefundTime { get; set; }
    }
}