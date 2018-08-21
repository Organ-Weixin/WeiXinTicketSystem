using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class WxMobilePhoneInfo
    {
        public string phoneNumber { get; set; }
        public string purePhoneNumber { get; set; }
        public string countryCode { get; set; }
        public WxMobilePhoneInfowatermark watermark { get; set; }
    }
    public class WxMobilePhoneInfowatermark
    {
        public string timestamp { get; set; }
        public string appid { get; set; }
    }
}