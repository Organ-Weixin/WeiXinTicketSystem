using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class CollectStampQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string StampCode { get; set; }
        public int CollectType { get; set; }
        public int Status { get; set; }
    }
}