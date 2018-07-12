using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitOrderQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public SubmitOrderQueryJsonOrder Order { get; set; }
    }
    public class SubmitOrderQueryJsonOrder
    {
        public string PaySeqNo { get; set; }
        public string OrderCode { get; set; }
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public string MobilePhone { get; set; }
        public List<SubmitOrderQueryJsonSeat> Seats { get; set; }
    }
    public class SubmitOrderQueryJsonSeat
    {
        public string SeatCode { get; set; }
        public decimal Price { get; set; }
        public decimal RealPrice { get; set; }
        public decimal Fee { get; set; }
    }
}