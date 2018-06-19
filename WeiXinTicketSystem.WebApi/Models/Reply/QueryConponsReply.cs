using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryConponsReply : BaseReply
    {
        public QueryConponsReplyConpons data { get; set; }
    }
    public class QueryConponsReplyConpons
    {
        public int ConponCount { get; set; }
        public List<QueryConponsReplyConpon> Conpons { get; set; }
    }
    public class QueryConponsReplyConpon
    {
        public int Id { get; set; }
        public string CinemaCode { get; set; }
        public string ConponType { get; set; }
        public string OpenID { get; set; }
        public decimal? Price { get; set; }
        public string ConponCode { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string Status { get; set; }
        public DateTime? UseDate { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}