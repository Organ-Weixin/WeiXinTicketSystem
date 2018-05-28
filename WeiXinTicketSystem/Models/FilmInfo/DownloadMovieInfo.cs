using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class DownloadMovieInfo
    {
        ///<summary>
        ///影片编码
        ///</summary>
        public string ID { get; set; }
        ///<summary>
        ///影片名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///影片发行版本
        ///</summary>
        public string Version { get; set; }

        ///<summary>
        ///影片时长
        ///</summary>
        public int Duration { get; set; }

        ///<summary>
        ///公映日期
        ///</summary>
        public DateTime PublishDate { get; set; }

        ///<summary>
        ///发行商
        ///</summary>
        public string Publisher { get; set; }

        ///<summary>
        ///制作人
        ///</summary>
        public string Producer { get; set; }

        ///<summary>
        ///导演
        ///</summary>
        public string Director { get; set; }

        ///<summary>
        ///演员
        ///</summary>
        public string Cast { get; set; }

        ///<summary>
        ///简介
        ///</summary>
        public string Introduction { get; set; }
        public string Brief { get; set; }
    }

    ///<summary>
    ///用于将Xml转换为类对象
    ///</summary>
    public class QryDownloadMovieInfo
    {
        public DownloadMovieInfo FilmInfomation { get; set; }
    }
}