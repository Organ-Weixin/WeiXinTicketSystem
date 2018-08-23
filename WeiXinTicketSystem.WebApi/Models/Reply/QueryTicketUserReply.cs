using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryTicketUserReply: BaseReply
    {
        public QueryTicketUserReplyTicketUser data { get; set; }
    }
    public class QueryTicketUserReplyTicketUser
    {
        public int TicketUserId { get; set; }
        public string CinemaCode { get; set; }
        public string MobilePhone { get; set; }
        public string OpenID { get; set; }
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string HeadImgUrl { get; set; }
        public string Language { get; set; }
        public long TotalScore { get; set; }
        public string IsActive { get; set; }
        public string Created { get; set; }

    }
}