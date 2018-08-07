using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class GivingConditionsQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        ///影院编码
        /// </summary>
        public string CinemaCode_dd { get; set; }

        /// <summary>
        /// 赠送条件
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// 优惠券类型
        /// </summary>
        public int ConponType_dd { get; set; }
    }
}