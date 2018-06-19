using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryUserOrdersReply:BaseReply
    {
        public QueryUserOrdersReplyOrders data { get; set; }
    }
    public class QueryUserOrdersReplyOrders
    {
        public string OpenID { get; set; }
        public int OrdersCount { get; set; }
        public List<QueryUserOrdersReplyOrder> Orders { get; set; }
    }
    public class QueryUserOrdersReplyOrder
    {
        public int OrderId { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public string MobilePhone { get; set; }
        public int SnacksCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime Created { get; set; }
    }
}