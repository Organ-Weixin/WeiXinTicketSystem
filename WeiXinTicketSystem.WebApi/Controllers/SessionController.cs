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
    public class SessionController : ApiController
    {
        SessionInfoService _sessionInfoService;
        SystemUserService _userService;
        CinemaService _cinemaService;

        #region ctor
        public SessionController()
        {
            _sessionInfoService = new SessionInfoService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
        }
        #endregion

        [HttpGet]
        public QuerySessionsReply QuerySessions(string UserName, string Password, string CinemaCode,string SessionTime)
        {
            QuerySessionsReply querySessionsReply = new QuerySessionsReply();

            //校验参数
            if (!querySessionsReply.RequestInfoGuard(UserName, Password, CinemaCode, SessionTime))
            {
                return querySessionsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                querySessionsReply.SetUserCredentialInvalidReply();
                return querySessionsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                querySessionsReply.SetCinemaInvalidReply();
                return querySessionsReply;
            }

            var Sessions = _sessionInfoService.GetSessions(CinemaCode,DateTime.Parse(SessionTime),DateTime.Parse(SessionTime));
            querySessionsReply.data = new QuerySessionsReplySessions();
            if (Sessions == null || Sessions.Count == 0)
            {
                querySessionsReply.data.SessionCount = 0;
            }
            else
            {
                querySessionsReply.data.SessionCount = Sessions.Count;
                querySessionsReply.data.Sessions = Sessions.Select(x => new QuerySessionsReplySession().MapFrom(x)).ToList();
            }
            querySessionsReply.SetSuccessReply();
            return querySessionsReply;
        }
    }
}