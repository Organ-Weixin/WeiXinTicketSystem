using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class BookSnacksReply: BaseReply
    {
        public BookSnacksReplySnacks data { get; set; }
    }
    public class BookSnacksReplySnacks
    {
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public string MobilePhone { get; set; }
        public int SnacksCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime AutoUnLockDateTime { get; set; }
        public List<BookSnacksReplySnack> Snacks { get; set; }

    }
    public class BookSnacksReplySnack
    {
        public string SnackCode { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Number { get; set; }
        public decimal SubTotalPrice { get; set; }
    }
}