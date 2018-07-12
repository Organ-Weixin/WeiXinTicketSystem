﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class RefundTicketReply: BaseReply
    {
        public RefundTicketReplyOrder data { get; set; }
    }
    public class RefundTicketReplyOrder
    {
        public string OrderCode { get; set; }
        public string PrintNo { get; set; }
        public string VerifyCode { get; set; }
        public YesOrNoEnum Status { get; set; }
        public string RefundTime { get; set; }
    }
}