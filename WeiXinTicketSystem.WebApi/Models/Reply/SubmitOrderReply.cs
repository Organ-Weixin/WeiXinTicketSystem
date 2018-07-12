using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitOrderReply:BaseReply
    {
        public SubmitOrderReplyOrder data { get; set; }
    }
    public class SubmitOrderReplyOrder
    {
        public CinemaTypeEnum CinemaType { get; set; }
        public string OrderCode { get; set; }
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public string PrintNo { get; set; }
        public string VerifyCode { get; set; }
        public List<SubmitOrderReplySeat> Seats { get; set; }
    }
    public class SubmitOrderReplySeat
    {
        public string SeatCode { get; set; }
        public string FilmTicketCode { get; set; }
    }
}