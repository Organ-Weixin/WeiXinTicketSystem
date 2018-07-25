using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class jscode2sessionReply
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string expires_in { get; set;}
    }
}