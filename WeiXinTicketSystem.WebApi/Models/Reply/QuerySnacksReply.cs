using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QuerySnacksReply: BaseReply
    {
        public QuerySnacksReplySnacks data { get; set; }
    }
    public class QuerySnacksReplySnacks
    {
        public int SnackCount { get; set; }
        public List<QuerySnacksReplySnack> Snacks { get; set; }
    }
    public class QuerySnacksReplySnack
    {
        public int SnackId { get; set; }
        public string CinemaCode { get; set; }
        public string SnackCode { get; set; }
        public int TypeId { get; set; }
        public string SnackName { get; set; }
        public string Remark { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Status { get; set; }
        public int Stock { get; set; }
        public string ExpDate { get; set; }
        public bool IsRecommand { get; set; }
        public string Image { get; set; }
    }
}