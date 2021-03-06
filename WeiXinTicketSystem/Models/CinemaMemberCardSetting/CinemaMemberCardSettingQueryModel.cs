﻿using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CinemaMemberCardSettingQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        ///影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 影院名称
        /// </summary>
        public string CinemaName { get; set; }
    }
}