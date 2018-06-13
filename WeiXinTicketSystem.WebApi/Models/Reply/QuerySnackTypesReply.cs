using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QuerySnackTypesReply: BaseReply
    {
        public QuerySnackTypesReplyTypes data { get; set; }
    }
    public class QuerySnackTypesReplyTypes
    {
        public int TypeCount { get; set; }
        public List<QuerySnackTypesReplyType> Types { get; set; }
    }
    public class QuerySnackTypesReplyType
    {
        public int Id { get; set; }
        public string CinemaCode { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }
        public string Image { get; set; }
    }
}