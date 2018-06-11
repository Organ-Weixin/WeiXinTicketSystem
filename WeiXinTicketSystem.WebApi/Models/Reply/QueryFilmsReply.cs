using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryFilmsReply : BaseReply
    {
        public QueryFilmsReplyFilm data { get; set; }
    }

    public class QueryFilmsReplyFilm
    {
        public int Id { get; set; }
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public string Version { get; set; }
        public string Duration { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Producer { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Introduction { get; set; }
        public decimal? Score { get; set; }
        public string Area { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string Trailer { get; set; }
    }
}