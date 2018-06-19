using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class SubmitFilmCommentQueryJson
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public decimal Score { get; set; }
        public string CommentContent { get; set; }
        public string OpenID { get; set; }
    }
}