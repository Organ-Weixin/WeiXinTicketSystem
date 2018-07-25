using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class WxUserInfo
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public watermark watermark { get; set; }
    }
    public class watermark
    {
        public long timestamp { get; set; }
        public string appid { get; set; }
    }
}