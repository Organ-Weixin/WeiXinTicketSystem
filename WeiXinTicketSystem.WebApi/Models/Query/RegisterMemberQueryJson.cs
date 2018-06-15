using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class RegisterMemberQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string CardNo { get; set; }
        public string CardPassword { get; set; }
        public decimal? Balance { get; set; }
        public int? Score { get; set; }
        public int MemberGrade { get; set; }
    }
}