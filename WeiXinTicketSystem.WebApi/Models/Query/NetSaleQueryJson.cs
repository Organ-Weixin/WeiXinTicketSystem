using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class NetSaleQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OpenID { get; set; }
        public string QueryXml { get; set; }
    }
}