using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryScreenSeatsReply : BaseReply
    {
        public QueryScreenSeatsReplySeats data { get; set; }
    }

    public class QueryScreenSeatsReplySeats
    {
        public int SeatCount { get; set; }
        public List<QueryScreenSeatsReplySeat> Seats { get; set; }
    }

    public class QueryScreenSeatsReplySeat
    {
        /// <summary>
        /// 影厅座位ID
        /// </summary>
        public int SeatId { get; set; }
        /// <summary>
        /// 影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 影厅编码
        /// </summary>
        public string ScreenCode { get; set; }
        /// <summary>
        /// 座位编码
        /// </summary>
        public string SeatCode { get; set; }
        /// <summary>
        /// 座位分组编码
        /// </summary>
        public string GroupCode { get; set; }
        /// <summary>
        /// 座位行号
        /// </summary>
        public string RowNum { get; set; }
        /// <summary>
        /// 座位列号
        /// </summary>
        public string ColumnNum { get; set; }
        /// <summary>
        /// 座位X坐标
        /// </summary>
        public int XCoord { get; set; }
        /// <summary>
        /// 座位Y坐标
        /// </summary>
        public int YCoord { get; set; }
        /// <summary>
        /// 座位状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 情侣座标志(N：普通座，L：情侣左，R：情侣右)
        /// </summary>
        public string LoveFlag { get; set; }
        /// <summary>
        /// 座位类型
        /// </summary>
        public string Type { get; set; }
    }
}