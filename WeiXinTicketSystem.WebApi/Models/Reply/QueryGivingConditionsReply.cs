using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryGivingConditionsReply : BaseReply
    {
        public QueryGivingConditionsReplyConditions data { get; set; }
    }

    public class QueryGivingConditionsReplyConditions
    {
        public int ConditionsCount { get; set; }
        public List<QueryGivingConditionsReplyCondition> Conditions { get; set; }
    }

    public class QueryGivingConditionsReplyCondition
    {
        public int ConditionId { get; set; }
        public string CinemaCode { get; set; }
        public string Conditions { get; set; }
        public string ConponType { get; set; }
        public decimal? Price { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}