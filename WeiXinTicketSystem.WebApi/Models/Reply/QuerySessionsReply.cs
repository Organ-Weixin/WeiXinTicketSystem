using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QuerySessionsReply : BaseReply
    {
        public QuerySessionsReplySessions data { get; set; }
    }

    public class QuerySessionsReplySessions
    {
        public int SessionsCount { get; set; }
        public List<QuerySessionsReplySession> Sessions { get; set; }
    }

    public class QuerySessionsReplySession
    {
        public int SessionId { get; set; }
        public string CinemaCode { get; set; }
        public string SessionCode { get; set; }
        public string ScreenCode { get; set; }
        public string StartTime { get; set; }
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public int? Duration { get; set; }
        public string Language { get; set; }
        public string UpdateTime { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal SettlePrice { get; set; }
        public decimal TicketFee { get; set; }
        public string IsAvalible { get; set; }
        public string Dimensional { get; set; }
        public decimal? ListingPrice { get; set; }
        public decimal SalePrice { get; set; }

    }
}