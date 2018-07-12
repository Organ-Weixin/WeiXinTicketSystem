using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class LockSeatReply: BaseReply
    {
        public LockSeatReplyOrder data { get; set; }
    }
    public class LockSeatReplyOrder
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public string AutoUnlockDatetime { get; set; }
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public List<LockSeatReplySeat> Seats { get; set; }
    }
    public class LockSeatReplySeat
    {
        public string SeatCode { get; set; }
    }
}