using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class ReleaseSeatQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public ReleaseSeatQueryJsonOrder Order { get; set; }
    }
    public class ReleaseSeatQueryJsonOrder
    {
        public string OrderCode { get; set; }
        public string SessionCode { get; set; }
        public int SeatsCount { get; set; }
        public List<ReleaseSeatQueryJsonSeat> Seats { get; set; }
    }
    public class ReleaseSeatQueryJsonSeat
    {
        public string SeatCode { get; set; }
    }
}