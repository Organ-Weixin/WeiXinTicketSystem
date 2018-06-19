using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SendConponQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public int ConponType { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidityDate { get; set; }
        public string Image { get; set; }
        public string OpenID { get; set; }

    }
}