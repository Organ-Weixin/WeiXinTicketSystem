using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SendConponReply: BaseReply
    {
        public SendConponReplyConpon data { get; set; }
    }
    public class SendConponReplyConpon
    {
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string ConponTypeCode { get; set; }
        public string ConponCode { get; set; }
        public decimal Price { get; set; }
        public string ValidityDate { get; set; }
        public string Image { get; set; }
        public string SendTime { get; set; }
        public string OpenID { get; set; }
    }
}