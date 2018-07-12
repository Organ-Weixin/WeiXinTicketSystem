using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class StampQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        /// 印章编码
        /// </summary>
        public string StampCode { get; set; }
    }
}