using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryUserStampsReply : BaseReply
    {
        public QueryUserStampsReplyStamps data { get; set; }
    }

    public class QueryUserStampsReplyStamps
    {
        public int UserStampCount { get; set; }
        public List<QueryUserStampsReplyStamp> UserStamps { get; set; }
    }
    public class QueryUserStampsReplyStamp
    {
        public int UserStampId { get; set; }
        public string CinemaCode { get; set; }
        public string OpenID { get; set; }
        public string CollectType { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public string StampCode { get; set; }
        public string StampTitle { get; set; }
        public string StampImage { get; set; }
        public DateTime? StampValidityDate { get; set; }

    }
}