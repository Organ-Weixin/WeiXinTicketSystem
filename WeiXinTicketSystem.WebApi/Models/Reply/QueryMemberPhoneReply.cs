using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryMemberPhoneReply : BaseReply
    {
        public QueryMemberPhoneReplyPhones data { get; set; }
    }

    public class QueryMemberPhoneReplyPhones
    {
        public int MemberPhoneCount { get; set; }
        public List<QueryMemberPhoneReplyPhone> MemberPhones { get; set; }
    }

    public class QueryMemberPhoneReplyPhone
    {
        public int MemberPhoneId { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string MobilePhone { get; set; }
        public string CardNo { get; set; }
        public string CardPassword { get; set; }
        public decimal? Balance { get; set; }
        public int? Score { get; set; }
        public string LevelCode { get; set; }
        public string LevelName { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string CreditNum { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Status { get; set; }
    }
}