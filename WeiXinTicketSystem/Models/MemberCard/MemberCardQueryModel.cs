using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class MemberCardQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        ///影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
    }
}