using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class ReleaseSnacksReply: BaseReply
    {
        public ReleaseSnacksReplyOrder data { get; set; }
    }
    public class ReleaseSnacksReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public DateTime ReleaseTime { get; set; }
    }
}