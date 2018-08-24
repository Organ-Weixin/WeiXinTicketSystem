using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryActivityPopupsReply : BaseReply
    {
        public QueryActivityPopupsReplyPopups data { get; set; }
    }

    public class QueryActivityPopupsReplyPopups
    {
        public int ActivityPopupCount { get; set; }
        public List<QueryActivityPopupsReplyPopup> ActivityPopups { get; set; }
    }

    public class QueryActivityPopupsReplyPopup
    {
        /// <summary>
        /// 活动弹窗ID
        /// </summary>
        public int ActivityPopupId { get; set; }
        /// <summary>
        /// 影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 弹窗类
        /// </summary>
        public string Popup { get; set; }
        /// <summary>
        /// 推荐类型编码
        /// </summary>
        public string GradeCode { get; set; }
        /// <summary>
        /// 推荐类型名称
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }
    }

}