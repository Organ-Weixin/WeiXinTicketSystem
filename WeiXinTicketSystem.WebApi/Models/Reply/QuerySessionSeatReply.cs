using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QuerySessionSeatReply: BaseReply
    {
        public QuerySessionSeatReplySessionSeat data { get; set; }
    }
    public class QuerySessionSeatReplySessionSeat
    {
        public string CinemaCode { get; set; }
        public string ScreenCode { get; set; }
        public List<QuerySessionSeatReplySeat> Seats { get; set; }
    }
    public class QuerySessionSeatReplySeat
    {
        public string SeatCode { get; set; }
        public string RowNum { get; set; }
        public string ColumnNum { get; set; }
        public string Status { get; set; }
    }
}