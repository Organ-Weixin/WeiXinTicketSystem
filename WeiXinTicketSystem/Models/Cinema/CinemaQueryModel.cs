using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CinemaQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        ///影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 影院名称
        /// </summary>
        public string CinemaName { get; set; }
        /// <summary>
        /// 影院是否开通接口
        /// </summary>
        public CinemaOpenEnum? IsOpen { get; set; }
    }
}