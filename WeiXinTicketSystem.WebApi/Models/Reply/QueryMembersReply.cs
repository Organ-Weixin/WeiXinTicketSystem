using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryMembersReply : BaseReply
    {
        public QueryMembersReplyMembers data { get; set; }
    }

    public class QueryMembersReplyMembers
    {
        public int MemberCount { get; set; }
        public List<QueryMembersReplyMember> Members { get; set; }
    }

    public class QueryMembersReplyMember
    {
        public int MemberId { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string CardNo { get; set; }
        public string CardPassword { get; set; }
        public decimal? Balance { get; set; }
        public int? Score { get; set; }
        public string MemberGrade { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }

    }
}