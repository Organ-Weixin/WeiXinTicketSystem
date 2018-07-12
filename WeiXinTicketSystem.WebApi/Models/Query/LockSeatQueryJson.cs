using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class LockSeatQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public LockSeatQueryJsonOrder Order { get; set; }
    }
    public class LockSeatQueryJsonOrder
    {
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public string PayType { get; set; }
        public List<LockSeatQueryJsonSeat> Seats { get; set; }
    }
    public class LockSeatQueryJsonSeat
    {
        public string SeatCode { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
    }
}