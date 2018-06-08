using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class BookSnacksQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string MobilePhone { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime SendTime { get; set; }
        public string OpenID { get; set; }
        public List<BookSnacksQueryJsonSnack> Snacks { get; set; }
    }
    public class BookSnacksQueryJsonSnack
    {
        public string SnackCode { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Number { get; set; }
        public decimal SubTotalPrice { get; set; }
    }
}