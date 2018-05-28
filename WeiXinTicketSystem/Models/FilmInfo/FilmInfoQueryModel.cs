using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class FilmInfoQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        /// 影片编码
        /// </summary>
        public string FilmCode { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string FilmName { get; set; }
    }
}