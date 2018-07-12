using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class ReleaseSeatReply: BaseReply
    {
        public ReleaseSeatReplyOrder data { get; set; }
    }
    public class ReleaseSeatReplyOrder
    {
        public string OrderCode { get; set; }
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public List<ReleaseSeatReplySeat> Seats { get; set; }
    }
    public class ReleaseSeatReplySeat
    {
        public string SeatCode { get; set; }
    }
}