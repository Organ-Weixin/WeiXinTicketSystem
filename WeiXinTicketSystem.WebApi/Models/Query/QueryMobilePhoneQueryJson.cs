using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryMobilePhoneQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CinemaCode { get; set; }
        public string Code { get; set; }
        public string EncryptedData { get; set; }
        public string Iv { get; set; }
    }
}