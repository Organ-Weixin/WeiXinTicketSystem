using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PrePayOrderQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public List<PrePayOrderQueryJsonSeat> Seats { get; set; }
    }
    public class PrePayOrderQueryJsonSeat
    {
        public string SeatCode { get; set; }
        public string ConponCode { get; set; }
        public decimal ConponPrice { get; set; }
    }
}