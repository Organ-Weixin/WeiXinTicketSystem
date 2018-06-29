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
    public class UserController : ApiController
    {
        ScoreRecordService _scoreRecordService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;

        #region ctor
        public UserController()
        {
            _scoreRecordService = new ScoreRecordService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
        }
        #endregion

        #region 用户签到


        [HttpPost]
        public SignInReply SignIn(SignInQueryJson QueryJson)
        {
           SignInReply signInReply = new SignInReply();
            //校验参数
            if (!signInReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OpenID, QueryJson.Type.ToString(), QueryJson.Score.ToString(), QueryJson.Description,QueryJson.Direction.ToString()))
            {
                return signInReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                signInReply.SetUserCredentialInvalidReply();
                return signInReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                signInReply.SetCinemaInvalidReply();
                return signInReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                signInReply.SetOpenIDNotExistReply();
                return signInReply;
            }


            //将请求参数转为影片评论信息
            var scoreRecord = new ScoreRecordEntity();
            scoreRecord.MapFrom(QueryJson);


            _scoreRecordService.Insert(scoreRecord);

            TicketUserEntity ticketUser = _ticketUserService.GetTicketUserByOpenID(scoreRecord.OpenID);
            if (scoreRecord.Direction == ScoreRecordDirectionEnum.Obtain)
            {
                if (ticketuser.TotalScore == null)
                {
                    ticketuser.TotalScore = 0;
                }
                ticketuser.TotalScore = ticketuser.TotalScore + scoreRecord.Score;
            }
            else if (scoreRecord.Direction == ScoreRecordDirectionEnum.Spend)
            {
                if (ticketuser.TotalScore == null)
                {
                    ticketuser.TotalScore = 0;
                }
                ticketuser.TotalScore = ticketuser.TotalScore - scoreRecord.Score;
            }
            _ticketUserService.Update(ticketuser);

            signInReply.data = new SignInReplySignIn();
            signInReply.data.MapFrom(scoreRecord);
            signInReply.SetSuccessReply();

            return signInReply;

        }

        #endregion
    }
}