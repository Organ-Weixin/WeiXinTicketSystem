using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryMemberChargeSettingReply : BaseReply
    {
        public QueryMemberChargeSettingReplySettings data { get; set; }
    }

    public class QueryMemberChargeSettingReplySettings
    {
        public int MemberChargeSettingCount { get; set; }
        public List<QueryMemberChargeSettingReplySetting> MemberChargeSettings { get; set; }
    }

    public class QueryMemberChargeSettingReplySetting
    {
        public int MemberChargeSettingId { get; set; }
        public string CinemaCode { get; set; }
        public decimal? Price { get; set; }
        public string TypeCode { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int? Number { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Remark { get; set; }
        public int? NotUsedNumber { get; set; }

    }
}