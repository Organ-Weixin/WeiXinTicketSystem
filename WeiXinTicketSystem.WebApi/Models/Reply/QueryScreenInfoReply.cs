﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryScreenInfoReply : BaseReply
    {
        public QueryScreenInfoReplyScreenInfo data { get; set; }
    }

    public class QueryScreenInfoReplyScreenInfo
    {
        /// <summary>
        /// 影厅ID
        /// </summary>
        public int ScreenId { get; set; }
        /// <summary>
        /// 影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 影厅编码
        /// </summary>
        public string ScreenCode { get; set; }
        /// <summary>
        /// 影厅名称
        /// </summary>
        public string ScreenName { get; set; }
        /// <summary>
        /// 影厅座位数量
        /// </summary>
        public int? SeatCount { get; set; }
        /// <summary>
        /// 影厅类型
        /// </summary>
        public string Type { get; set; }

    }
}