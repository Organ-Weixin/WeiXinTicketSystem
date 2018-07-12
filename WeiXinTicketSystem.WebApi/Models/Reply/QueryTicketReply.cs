using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryTicketReply:BaseReply
    {
        public QueryTicketReplyTickets data { get; set; }
    }
    public class QueryTicketReplyTickets
    {
        public int TicketsCount { get; set; }
        public List<QueryTicketReplyTicket> Tickets { get; set; }
    }
    public class QueryTicketReplyTicket
    {
        public string PrintNo { get; set; }
        public string TicketInfoCode { get; set; }
        public string CinemaCode { get; set; }
        public string CinemaName { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenName { get; set; }
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public string SessionCode { get; set; }
        public string SessionDateTime { get; set; }
        public string TicketCode { get; set; }
        public string SeatCode { get; set; }
        public string SeatName { get; set; }
        public string Price { get; set; }
        public string Service { get; set; }
        public string PrintFlag { get; set; }
    }
}