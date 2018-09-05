using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QuerySnackInfoReply : BaseReply
    {
        public QuerySnackInfoReplySnackInfo data { get; set; }
    }

    public class QuerySnackInfoReplySnackInfo
    {
        public int SnackId { get; set; }
        public string CinemaCode { get; set; }
        public string SnackCode { get; set; }
        public string TypeCode { get; set; }
        public string SnackName { get; set; }
        public string Remark { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Status { get; set; }
        public int Stock { get; set; }
        public string ExpDate { get; set; }
        public string IsRecommand { get; set; }
        public string Image { get; set; }
    }
}