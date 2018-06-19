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
        public int OrderId { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public string MobilePhone { get; set; }
        public int SnacksCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReleaseTime { get; set; }
        public string SubmitTime { get; set; }
        public string VoucherCode { get; set; }
        public string OrderStatus { get; set; }
        public string RefundTime { get; set; }
        public string FetchTime { get; set; }
        public string Created { get; set; }
        public string AutoUnLockDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public string SendTime { get; set; }
        public bool OrderPayFlag { get; set; }
        public string OrderPayType { get; set; }
        public string OrderPayTime { get; set; }
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