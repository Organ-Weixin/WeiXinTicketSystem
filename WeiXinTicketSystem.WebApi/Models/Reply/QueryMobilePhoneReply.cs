﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryMobilePhoneReply: BaseReply
    {
        public QueryMobilePhoneReplyData data { get; set; }
    }
    public class QueryMobilePhoneReplyData
    {
        public string phoneNumber { get; set; }
        public string purePhoneNumber { get; set; }
        public string countryCode { get; set; }
        public QueryMobilePhoneReplyWatermark watermark { get; set; }
    }
    public class QueryMobilePhoneReplyWatermark
    {
        public string timestamp { get; set; }
        public string appid { get; set; }
    }
}