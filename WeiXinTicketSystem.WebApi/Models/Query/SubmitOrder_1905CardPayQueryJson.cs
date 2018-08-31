using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitOrder_1905CardPayQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string CardNo { get; set; }
        public string CardPassword { get; set; }
        public string OrderCode { get; set;}
        public decimal LowestPrice { get; set; }
        public List<SubmitOrder_1905CardPayQueryJsonSeat> Seats { get; set; }
    }
    public class SubmitOrder_1905CardPayQueryJsonSeat
    {
        public string SeatCode { get; set; }
        public decimal MemberPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
    }
}