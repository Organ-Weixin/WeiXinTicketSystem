﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryActivityReply : BaseReply
    {
        public QueryActivityReplyActivitys data { get; set; }
    }
    public class QueryActivityReplyActivitys
    {
        public int ActivityCount { get; set; }
        public List<QueryActivityReplyActivity> Activitys { get; set; }
    }
    public class QueryActivityReplyActivity
    {
        public int Id { get; set; }
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ActivityContent { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public bool IsDel { get; set; }
    }
}