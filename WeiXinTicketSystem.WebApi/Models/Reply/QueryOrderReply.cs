using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryOrderReply: BaseReply
    {
        public QueryOrderReplyOrder data { get; set; }
    }
    public class QueryOrderReplyOrder
    {
        public string OrderCode { get; set; }
        public string CinemaCode { get; set; }
        public CinemaTypeEnum CinemaType { get; set; }
        public string CinemaName { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenName { get; set; }
        public string SessionCode { get; set; }
        public string StartTime { get; set; }
        public string PlaythroughFlag { get; set; }
        public string PrintNo { get; set; }
        public string VerifyCode { get; set; }
        public QueryOrderReplyFilm Film { get; set; }
        public List<QueryOrderReplySeat> Seats { get; set; }
    }

    public class QueryOrderReplyFilm
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Sequence { get; set; }
    }
    public class QueryOrderReplySeat
    {
        public string SeatCode { get; set; }
        public string RowNum { get; set; }
        public string ColumnNum { get; set; }
        public string FilmTicketCode { get; set; }
        public YesOrNoEnum PrintStatus { get; set; }
        public string PrintTime { get; set; }
        public YesOrNoEnum RefundStatus { get; set; }
        public string RefundTime { get; set; }
    }
}