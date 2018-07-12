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
        SeatInfoService _seatInfoService;

        #region ctor
        public SessionController()
        {
            _sessionInfoService = new SessionInfoService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _seatInfoService = new SeatInfoService();
        }
        #endregion

        [HttpGet]
        public QuerySessionsReply QuerySessions(string UserName, string Password, string CinemaCode,string StartDate,string EndDate)
        {
            QuerySessionsReply querySessionsReply = new QuerySessionsReply();

            //校验参数
            if (!querySessionsReply.RequestInfoGuard(UserName, Password, CinemaCode, StartDate, EndDate))
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
            //验证日期是否正确
            DateTime Start, End;
            if (!DateTime.TryParse(StartDate, out Start))
            {
                querySessionsReply.SetStartDateInvalidReply();
                return querySessionsReply;
            }
            if (!DateTime.TryParse(EndDate, out End))
            {
                querySessionsReply.SetEndDateInvalidReply();
                return querySessionsReply;
            }
            if (Start > End)
            {
                querySessionsReply.SetDateInvalidReply();
                return querySessionsReply;
            }

            var Sessions = _sessionInfoService.GetSessions(CinemaCode,DateTime.Parse(StartDate),DateTime.Parse(EndDate));
            //如果排期为空，重新获取一遍
            if (Sessions==null || Sessions.Count==0)
            {
                _sessionInfoService.QuerySession(CinemaCode, DateTime.Parse(StartDate), DateTime.Parse(EndDate));
                Sessions= _sessionInfoService.GetSessions(CinemaCode, DateTime.Parse(StartDate), DateTime.Parse(EndDate));
            }
            querySessionsReply.data = new QuerySessionsReplySessions();
            if (Sessions == null || Sessions.Count == 0)
            {
                querySessionsReply.data.SessionsCount = 0;
            }
            else
            {
                querySessionsReply.data.SessionsCount = Sessions.Count;
                querySessionsReply.data.Sessions = Sessions.Select(x => new QuerySessionsReplySession().MapFrom(x)).ToList();
            }
            querySessionsReply.SetSuccessReply();
            return querySessionsReply;
        }

        [HttpGet]
        public QuerySessionSeatReply QuerySessionSeat(string UserName, string Password, string CinemaCode, string SessionCode, string Status)
        {
            QuerySessionSeatReply querySessionSeatReply = new QuerySessionSeatReply();
            //校验参数
            if (!querySessionSeatReply.RequestInfoGuard(UserName, Password, CinemaCode, SessionCode, Status))
            {
                return querySessionSeatReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                querySessionSeatReply.SetUserCredentialInvalidReply();
                return querySessionSeatReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                querySessionSeatReply.SetCinemaInvalidReply();
                return querySessionSeatReply;
            }
            //验证排期是否存在
            var sessionInfo = _sessionInfoService.GetSessionInfo(CinemaCode, SessionCode);
            if (sessionInfo == null)
            {
                querySessionSeatReply.SetSessionInvalidReply();
                return querySessionSeatReply;
            }
            //验证座位售出状态
            var StatusEnum = Status.CastToEnum<SessionSeatStatusEnum>();
            if (StatusEnum == default(SessionSeatStatusEnum))
            {
                querySessionSeatReply.SetSessionSeatStatusInvalidReply();
                return querySessionSeatReply;
            }

            var netSaleQuerySessionSeatReply = _seatInfoService.QuerySessionSeat(CinemaCode, SessionCode, Status);
            querySessionSeatReply.data = new QuerySessionSeatReplySessionSeat();
            if(netSaleQuerySessionSeatReply!=null&&netSaleQuerySessionSeatReply.SessionSeat.Seat.Count>0)
            {
                querySessionSeatReply.data.CinemaCode = CinemaCode;
                querySessionSeatReply.data.ScreenCode = sessionInfo.ScreenCode;
                querySessionSeatReply.data.Seats= netSaleQuerySessionSeatReply.SessionSeat.Seat.Select(x => new QuerySessionSeatReplySeat().MapFrom(x)).ToList();

            }
            querySessionSeatReply.SetSuccessReply();
            return querySessionSeatReply;
        }
    }
}