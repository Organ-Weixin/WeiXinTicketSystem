using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitSnacksReply: BaseReply
    {
        public SubmitSnacksReplyOrder data { get; set; }
    }
    public class SubmitSnacksReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public DateTime SubmitTime { get; set; }
        public string VoucherCode { get; set; }
    }
}