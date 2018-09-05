using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PrePaySnackOrderReply: BaseReply
    {
        public PrePaySnackOrderReplyParameters data { get; set; }
    }
    public class PrePaySnackOrderReplyParameters
    {
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }
        public string signType { get; set; }
        public string paySign { get; set; }
    }
}