using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryFilmCommentsReply : BaseReply
    {
        public QueryFilmCommentsReplyComments data { get; set; }
    }

    public class QueryFilmCommentsReplyComments
    {
        public int CommentCount { get; set; }
        public List<QueryFilmCommentsReplyComment> Comments { get; set; }
    }

    public class QueryFilmCommentsReplyComment
    {
        public int CommentId { get; set; }
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public decimal Score { get; set; }
        public string CommentContent { get; set; }
        public string OpenID { get; set; }
        public DateTime Created { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
 
    }
}