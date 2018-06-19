using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryBannersReply:BaseReply
    {
        public QueryBannersReplyBanners data { get; set; }
    }
    public class QueryBannersReplyBanners
    {
        public int BannersCount { get; set; }
        public List<QueryBannersReplyBanner> Banners { get; set; }
    }
    public class QueryBannersReplyBanner
    {
        public int BannerId { get; set; }
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Created { get; set; }
    }
}