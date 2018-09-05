using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class PrePaySnackOrderQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string OrderCode { get; set; }
        public List<PrePaySnackOrderQueryJsonSnack> Snacks { get; set; }
    }
    public class PrePaySnackOrderQueryJsonSnack
    {
        public string SnackCode { get; set; }
        public string ConponCode { get; set; }
        public decimal ConponPrice { get; set; }
    }
}