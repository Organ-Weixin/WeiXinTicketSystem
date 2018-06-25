using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WeiXinTicketSystem.Service;
using WeiXinTicketSystem.Entity;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.WebApi.Models;
using WeiXinTicketSystem.WebApi.Extension;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Controllers
{
    public class CommentController : ApiController
    {
        FilmCommentService _filmCommentService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;
        FilmInfoService _filmInfoService;

        #region ctor
        public CommentController()
        {
            _filmCommentService = new FilmCommentService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
            _filmInfoService = new FilmInfoService();
        }
        #endregion


        #region 提交影片评论

        [HttpPost]
        public SubmitFilmCommentReply SubmitFilmComment(SubmitFilmCommentQueryJson QueryJson)
        {
            SubmitFilmCommentReply submitFilmCommentReply = new SubmitFilmCommentReply();
            //校验参数
            if (!submitFilmCommentReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.FilmCode, QueryJson.FilmName, QueryJson.Score.ToString(), QueryJson.CommentContent, QueryJson.OpenID))
            {
                return submitFilmCommentReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                submitFilmCommentReply.SetUserCredentialInvalidReply();
                return submitFilmCommentReply;
            }
            //验证影片编码是否存在
            FilmInfoEntity filmInfo = _filmInfoService.GetFilmInfoByFilmCode(QueryJson.FilmCode);
            if (filmInfo == null)
            {
                submitFilmCommentReply.SetFilmCodeNotExistReply();
                return submitFilmCommentReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                submitFilmCommentReply.SetOpenIDNotExistReply();
                return submitFilmCommentReply;
            }


            //将请求参数转为影片评论信息
            var filmComment = new FilmCommentEntity();
            filmComment.MapFrom(QueryJson);


           _filmCommentService.Insert(filmComment);

            submitFilmCommentReply.data = new SubmitFilmCommentReplyComment();
            submitFilmCommentReply.data.MapFrom(filmComment);
            submitFilmCommentReply.SetSuccessReply();

            return submitFilmCommentReply;

        }

        #endregion


        #region 获取影片评论信息

        [HttpGet]
        public async Task<QueryFilmCommentsReply> QueryFilmComments(string UserName, string Password, string CinemaCode, string FilmCode, string CurrentPage, string PageSize)
        {
            QueryFilmCommentsReply queryFilmCommentsReply = new QueryFilmCommentsReply();
            //校验参数
            if (!queryFilmCommentsReply.RequestInfoGuard(UserName, Password, CinemaCode,FilmCode, CurrentPage, PageSize))
            {
                return queryFilmCommentsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryFilmCommentsReply.SetUserCredentialInvalidReply();
                return queryFilmCommentsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryFilmCommentsReply.SetCinemaInvalidReply();
                return queryFilmCommentsReply;
            }
            //验证影片编码是否存在
            FilmInfoEntity filmInfo = _filmInfoService.GetFilmInfoByFilmCode(FilmCode);
            if (filmInfo == null)
            {
                queryFilmCommentsReply.SetFilmCodeNotExistReply();
                return queryFilmCommentsReply;
            }

            var FilmComments = await _filmCommentService.QueryFilmCommentsPagedAsync(FilmCode, int.Parse(CurrentPage), int.Parse(PageSize));

            queryFilmCommentsReply.data = new QueryFilmCommentsReplyComments();
            if (FilmComments == null || FilmComments.Count == 0)
            {
                queryFilmCommentsReply.data.CommentCount = 0;
            }
            else
            {
                queryFilmCommentsReply.data.CommentCount = FilmComments.Count;
                queryFilmCommentsReply.data.Comments = FilmComments.Select(x => new QueryFilmCommentsReplyComment().MapFrom(x)).ToList();
            }
            queryFilmCommentsReply.SetSuccessReply();
            return queryFilmCommentsReply;
        }

        #endregion
    }
}