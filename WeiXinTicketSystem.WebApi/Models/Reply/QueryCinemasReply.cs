﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryCinemasReply : BaseReply
    {
        public QueryCinemasReplyCinemas data { get; set; }
    }

    public class QueryCinemasReplyCinemas
    {
        public int CinemaCount { get; set; }
        public List<QueryCinemasReplyCinema> Cinemas { get; set; }
    }

    public class QueryCinemasReplyCinema
    {
        public int CinemaId { get; set; }
        public string CinemaCode { get; set; }
        public string CinemaName { get; set; }
        public string CinemaType { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string TheaterChain { get; set; }
        public string Address { get; set; }
        public string IsOpen { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string OpenSnacks { get; set; }
        public string TicketHint { get; set; }
        public string CinemaLabel { get; set; }
        public string CinemaPhone { get; set; }
        public string IsSnackDistribution { get; set; }

    }
}