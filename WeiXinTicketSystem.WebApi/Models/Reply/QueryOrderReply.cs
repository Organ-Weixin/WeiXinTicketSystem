using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryOrderReply: BaseReply
    {
        public QueryOrderReplyOrder data { get; set; }
    }
    public class QueryOrderReplyOrder
    {
        public int Id { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public string MobilePhone { get; set; }
        public int SnacksCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReleaseTime { get; set; }
        public DateTime SubmitTime { get; set; }
        public string VoucherCode { get; set; }
        public SnackOrderStatusEnum OrderStatus { get; set; }
        public DateTime RefundTime { get; set; }
        public DateTime FetchTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime AutoUnLockDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime SendTime { get; set; }
        public bool OrderPayFlag { get; set; }
        public byte OrderPayType { get; set; }
        public DateTime OrderPayTime { get; set; }
        public string OrderTradeNo { get; set; }
        public bool IsUseConpons { get; set; }
        public string ConponCode { get; set; }
        public decimal ConponPrice { get; set; }
        public string OpenID { get; set; }
        public List<QueryOrderReplySnack> Snacks { get; set; }
    }
    public class QueryOrderReplySnack
    {
        public string SnackCode { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Number { get; set; }
        public decimal SubTotalPrice { get; set; }

    }
}