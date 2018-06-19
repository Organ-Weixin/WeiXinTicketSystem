using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitFilmCommentReply : BaseReply
    {
        public SubmitFilmCommentReplyComment data { get; set; }
    }

    public class SubmitFilmCommentReplyComment
    {
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public decimal Score { get; set; }
        public string CommentContent { get; set; }
        public string OpenID { get; set; }
        public DateTime Created { get; set; }


    }
}