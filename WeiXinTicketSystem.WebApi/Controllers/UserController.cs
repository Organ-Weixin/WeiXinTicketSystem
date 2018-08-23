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
        UserStampService _userStampService;
        StampService _stampService;
        CinemaMiniProgramAccountService _miniProgramAccountService;

        #region ctor
        public UserController()
        {
            _scoreRecordService = new ScoreRecordService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
            _userStampService = new UserStampService();
            _stampService = new StampService();
            _miniProgramAccountService = new CinemaMiniProgramAccountService();
        }
        #endregion

        #region 用户登录
        [HttpPost]
        public TicketUserLoginReply UserLogin(TicketUserLoginQueryJson QueryJson)
        {
            TicketUserLoginReply ticketUserLoginReply = new TicketUserLoginReply();
            //校验参数
            if (!ticketUserLoginReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.Code, QueryJson.EncryptedData, QueryJson.Iv))
            {
                return ticketUserLoginReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                ticketUserLoginReply.SetUserCredentialInvalidReply();
                return ticketUserLoginReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                ticketUserLoginReply.SetCinemaInvalidReply();
                return ticketUserLoginReply;
            }
            //验证并获取AppID和AppSecret
            var miniProgramAccount = _miniProgramAccountService.GetCinemaMiniProgramAccountByCinemaCode(QueryJson.CinemaCode);
            if (miniProgramAccount == null)
            {
                ticketUserLoginReply.SetCinemaMiniProgramAccountInvalidReply();
                return ticketUserLoginReply;
            }
            //获取sessionKey
            string url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", miniProgramAccount.AppId, miniProgramAccount.AppSecret, QueryJson.Code);
            string returnStr = HttpUtil.Send("", url);
            jscode2sessionReply jscode2sessionReply = returnStr.JsonDeserialize<jscode2sessionReply>();
            //string openid = jscode2sessionReply.openid;
            string sessionkey = jscode2sessionReply.session_key;

            string swxUserInfo = AESHelper.AesDecrypt(QueryJson.EncryptedData, sessionkey, QueryJson.Iv);
            WxUserInfo wxUserInfo = swxUserInfo.JsonDeserialize<WxUserInfo>();

            var ticketUser = _ticketUserService.GetTicketUserByOpenID(wxUserInfo.openId);
            if (ticketUser == null)
            {
                ticketUser = new TicketUserEntity();
                ticketUser.MapFrom(wxUserInfo);
                ticketUser.CinemaCode = QueryJson.CinemaCode;
                ticketUser.IsActive = YesOrNoEnum.Yes;
                ticketUser.Created = DateTime.Now;
                ticketUser.TotalScore = 0;
                ticketUser.Id = _ticketUserService.Insert(ticketUser);
            }
            else
            {
                ticketUser.MapFrom(wxUserInfo);
                _ticketUserService.Update(ticketUser);
            }

            ticketUserLoginReply.data = new TicketUserLoginTicketUser();
            ticketUserLoginReply.data.MapFrom(ticketUser);
            ticketUserLoginReply.SetSuccessReply();
            return ticketUserLoginReply;
        }
        #endregion

        #region 获取手机号
        [HttpPost]
        public QueryMobilePhoneReply QueryMobilePhone(QueryMobilePhoneQueryJson QueryJson)
        {
            QueryMobilePhoneReply queryMobilePhoneReply = new QueryMobilePhoneReply();
            //校验参数
            if (!queryMobilePhoneReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.Code, QueryJson.EncryptedData, QueryJson.Iv))
            {
                return queryMobilePhoneReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                queryMobilePhoneReply.SetUserCredentialInvalidReply();
                return queryMobilePhoneReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                queryMobilePhoneReply.SetCinemaInvalidReply();
                return queryMobilePhoneReply;
            }
            //验证并获取AppID和AppSecret
            var miniProgramAccount = _miniProgramAccountService.GetCinemaMiniProgramAccountByCinemaCode(QueryJson.CinemaCode);
            if (miniProgramAccount == null)
            {
                queryMobilePhoneReply.SetCinemaMiniProgramAccountInvalidReply();
                return queryMobilePhoneReply;
            }
            //验证用户OpenID是否存在
            var ticketUser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if(ticketUser==null)
            {
                queryMobilePhoneReply.SetOpenIDNotExistReply();
                return queryMobilePhoneReply;
            }
            //获取sessionKey
            string url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", miniProgramAccount.AppId, miniProgramAccount.AppSecret, QueryJson.Code);
            string returnStr = HttpUtil.Send("", url);
            jscode2sessionReply jscode2sessionReply = returnStr.JsonDeserialize<jscode2sessionReply>();
            string sessionkey = jscode2sessionReply.session_key;

            string swxMobilePhoneInfo = AESHelper.AesDecrypt(QueryJson.EncryptedData, sessionkey, QueryJson.Iv);
            //string swxMobilePhoneInfo = "{ \"phoneNumber\":\"15058598907\",\"purePhoneNumber\":\"15058598907\",\"countryCode\":\"86\",\"watermark\":{ \"timestamp\":1534814999,\"appid\":\"wxe9ac67c34cccb15d\"} }";
            WxMobilePhoneInfo wxMobilePhoneInfo = swxMobilePhoneInfo.JsonDeserialize<WxMobilePhoneInfo>();
            //把手机号更新到用户表
            ticketUser.MobilePhone = wxMobilePhoneInfo.phoneNumber;
            _ticketUserService.Update(ticketUser);

            queryMobilePhoneReply.data = new QueryMobilePhoneReplyData();
            queryMobilePhoneReply.data.MapFrom(wxMobilePhoneInfo);
            queryMobilePhoneReply.SetSuccessReply();
            return queryMobilePhoneReply;
        }
        #endregion

        #region 用户签到


        [HttpPost]
        public SignInReply SignIn(SignInQueryJson QueryJson)
        {
            SignInReply signInReply = new SignInReply();
            //校验参数
            if (!signInReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OpenID, QueryJson.Type.ToString(), QueryJson.Score.ToString(), QueryJson.Description, QueryJson.Direction.ToString()))
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

            if (scoreRecord.Direction == ScoreRecordDirectionEnum.Obtain)
            {
                ticketuser.TotalScore = ticketuser.TotalScore ?? 0 + scoreRecord.Score;
            }
            else if (scoreRecord.Direction == ScoreRecordDirectionEnum.Spend)
            {
                ticketuser.TotalScore = ticketuser.TotalScore > scoreRecord.Score ? ticketuser.TotalScore - scoreRecord.Score : 0;
            }
            _ticketUserService.Update(ticketuser);

            signInReply.data = new SignInReplySignIn();
            signInReply.data.MapFrom(scoreRecord, ticketuser);
            signInReply.SetSuccessReply();

            return signInReply;
        }

        #endregion

        #region 收集印章

        [HttpPost]
        public CollectStampReply CollectStamp(CollectStampQueryJson QueryJson)
        {
            CollectStampReply collectStampReply = new CollectStampReply();
            //校验参数
            if (!collectStampReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OpenID, QueryJson.StampCode, QueryJson.CollectType.ToString(), QueryJson.Status.ToString()))
            {
                return collectStampReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                collectStampReply.SetUserCredentialInvalidReply();
                return collectStampReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                collectStampReply.SetCinemaInvalidReply();
                return collectStampReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                collectStampReply.SetOpenIDNotExistReply();
                return collectStampReply;
            }


            //将请求参数转为用户印章信息
            var userStamp = new UserStampEntity();
            userStamp.MapFrom(QueryJson);

            _userStampService.Insert(userStamp);

            collectStampReply.data = new CollectStampReplyStamp();
            StampEntity stamp = _stampService.GetStampByStampCode(userStamp.StampCode);
            collectStampReply.data.MapFrom(userStamp, stamp);
            collectStampReply.SetSuccessReply();

            return collectStampReply;

        }

        #endregion

        #region 查询用户印章

        [HttpGet]
        public async Task<QueryUserStampsReply> QueryUserStamps(string UserName, string Password, string CinemaCode, string OpenID)
        {
            QueryUserStampsReply queryUserStampsReply = new QueryUserStampsReply();

            //校验参数
            if (!queryUserStampsReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID))
            {
                return queryUserStampsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryUserStampsReply.SetUserCredentialInvalidReply();
                return queryUserStampsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryUserStampsReply.SetCinemaInvalidReply();
                return queryUserStampsReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryUserStampsReply.SetOpenIDNotExistReply();
                return queryUserStampsReply;
            }
            var userStamps = await _userStampService.GetApiUserStampViewByOpenIDAsync(OpenID);
            queryUserStampsReply.data = new QueryUserStampsReplyStamps();
            if (userStamps == null || userStamps.Count == 0)
            {
                queryUserStampsReply.data.UserStampCount = 0;
            }
            else
            {
                queryUserStampsReply.data.UserStampCount = userStamps.Count;
                queryUserStampsReply.data.UserStamps = userStamps.Select(x => new QueryUserStampsReplyStamp().MapFrom(x)).ToList();
            }
            queryUserStampsReply.SetSuccessReply();
            return queryUserStampsReply;
        }

        #endregion

        #region 查询购票用户信息
        [HttpGet]
        public QueryTicketUserReply QueryUser(string UserName, string Password, string CinemaCode, string OpenID)
        {
            QueryTicketUserReply queryTicketUserReply = new QueryTicketUserReply();
            //校验参数
            if (!queryTicketUserReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID))
            {
                return queryTicketUserReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryTicketUserReply.SetUserCredentialInvalidReply();
                return queryTicketUserReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryTicketUserReply.SetCinemaInvalidReply();
                return queryTicketUserReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryTicketUserReply.SetOpenIDNotExistReply();
                return queryTicketUserReply;
            }
            queryTicketUserReply.data = new QueryTicketUserReplyTicketUser();
            queryTicketUserReply.data.MapFrom(ticketuser);
            queryTicketUserReply.SetSuccessReply();
            return queryTicketUserReply;
        }
        #endregion

    }
}