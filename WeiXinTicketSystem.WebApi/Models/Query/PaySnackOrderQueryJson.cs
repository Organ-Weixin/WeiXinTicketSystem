using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PaySnackOrderQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public int OrderPayType { get; set; }
        public DateTime OrderPayTime { get; set; }
        public string OrderTradeNo { get; set; }
        public bool IsUseConpons { get; set; }
        public string ConponCode { get; set; }
        public decimal ConponPrice { get; set; }
        public string OpenID { get; set; }
    }
}