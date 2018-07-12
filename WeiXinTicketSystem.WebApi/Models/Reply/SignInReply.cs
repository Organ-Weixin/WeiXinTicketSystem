using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SignInReply : BaseReply
    {
        public SignInReplySignIn data { get; set; }
    }

    public class SignInReplySignIn
    {
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string Type { get; set; }
        public int? Score { get; set; }
        public string Description { get; set; }
        public string Direction { get; set; }
        public DateTime? Created { get; set; }
        public long? TotalScore { get; set; }


    }
}