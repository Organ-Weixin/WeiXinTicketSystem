using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class FetchSnacksReply:BaseReply
    {
        public FetchSnacksReplyOrder data { get; set; }
    }
    public class FetchSnacksReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public DateTime FetchTime { get; set; }
    }
}