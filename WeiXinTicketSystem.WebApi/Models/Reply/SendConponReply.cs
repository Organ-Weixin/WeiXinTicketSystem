using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SendConponReply: BaseReply
    {
        public SendConponReplyConpons data { get; set; }
    }
    public class SendConponReplyConpons
    {
        public int ConponCount { get; set; }
        public List<SendConponReplyConpon> Conpons { get; set; }
    }
    public class SendConponReplyConpon
    {
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string ConponTypeCode { get; set; }
        public string ConponTypeName { get; set; }
        public string ConponCode { get; set; }
        public decimal Price { get; set; }
        public string ValidityDate { get; set; }
        public string Image { get; set; }
    }
}