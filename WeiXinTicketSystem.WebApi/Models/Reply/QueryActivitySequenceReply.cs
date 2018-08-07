using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryActivitySequenceReply : BaseReply
    {
        public QueryActivitySequenceReplyActivity data { get; set; }
    }

    public class QueryActivitySequenceReplyActivity
    {
        public int ActivityId { get; set; }
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ActivityContent { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string LinkUrl { get; set; }
        public string GradeCode { get; set; }
        public string ActivitySequence { get; set; }
        public string Status { get; set; }
    }
}