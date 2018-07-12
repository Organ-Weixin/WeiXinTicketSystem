using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SignInQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public int Type { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public int Direction { get; set; }
    }
}