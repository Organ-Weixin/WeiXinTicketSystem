using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.FilmInfo
{
    public class DoubanFilmReply
    {
        /// <summary>
        /// 评分
        /// </summary>
        public rating rating { get; set; }
        /// <summary>
        /// 影评数
        /// </summary>
        public int reviews_count { get; set; }
        /// <summary>
        /// 这么多人想看
        /// </summary>
        public int wish_count { get; set; }
        /// <summary>
        /// 这么多人看过
        /// </summary>
        public int collect_count { get; set; }
        /// <summary>
        /// 豆瓣专题网址
        /// </summary>
        public string douban_site { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string year { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public images images { get; set; }
        /// <summary>
        /// 影片地址
        /// </summary>
        public string alt { get; set; }
        /// <summary>
        /// 影片编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 影片地址
        /// </summary>
        public string mobile_url { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string title { get; set; }
        public string do_count { get; set; }
        /// <summary>
        /// 影片地址
        /// </summary>
        public string share_url { get; set; }
        /// <summary>
        /// 季数
        /// </summary>
        public string seasons_count { get; set; }
        public string schedule_url { get; set; }
        /// <summary>
        /// 集数
        /// </summary>
        public string episodes_count { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string[] genres { get; set; }
        /// <summary>
        /// 国家地区
        /// </summary>
        public string[] countries { get; set; }
        /// <summary>
        /// 演员
        /// </summary>
        public List<cast> casts { get; set; }
        /// <summary>
        /// 当前季数
        /// </summary>
        public string current_season { get; set; }
        /// <summary>
        /// 旧名
        /// </summary>
        public string original_title { get; set; }
        /// <summary>
        /// 影片介绍
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 子类型
        /// </summary>
        public string subtype { get; set; }
        /// <summary>
        /// 导演
        /// </summary>
        public List<director> directors { get; set; }
        /// <summary>
        /// 短评数
        /// </summary>
        public int comments_count { get; set; }
        /// <summary>
        /// 评价人数
        /// </summary>
        public int ratings_count { get; set; }
        /// <summary>
        /// 又名
        /// </summary>
        public string[] aka { get; set; }
    }
}