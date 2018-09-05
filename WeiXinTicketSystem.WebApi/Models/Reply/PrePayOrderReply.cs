using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PrePayOrderReply : BaseReply
    {
        public PrePayOrderReplyParameters data { get; set; }
    }
    public class PrePayOrderReplyParameters
    {
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }
        public string signType { get; set; }
        public string paySign { get; set; }
    }
}