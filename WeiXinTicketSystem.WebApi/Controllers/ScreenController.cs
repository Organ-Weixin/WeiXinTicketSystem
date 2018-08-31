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
    public class ScreenController : ApiController
    {
        ScreenInfoService _screenInfoService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;
        SeatInfoService _seatInfoService;

        #region ctor
        public ScreenController()
        {
            _screenInfoService = new ScreenInfoService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
            _seatInfoService = new SeatInfoService();
        }
        #endregion

        #region 获取影厅信息列表

        [HttpGet]
        public QueryScreensReply QueryScreens(string UserName, string Password, string CinemaCode)
        {
            QueryScreensReply queryScreensReply = new QueryScreensReply();
            //校验参数
            if (!queryScreensReply.RequestInfoGuard(UserName, Password, CinemaCode))
            {
                return queryScreensReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryScreensReply.SetUserCredentialInvalidReply();
                return queryScreensReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryScreensReply.SetCinemaInvalidReply();
                return queryScreensReply;
            }

            var Screens = _screenInfoService.GetScreenListByCinemaCode(CinemaCode);

            queryScreensReply.data = new QueryScreensReplyScreens();
            if (Screens == null || Screens.Count == 0)
            {
                queryScreensReply.data.ScreenCount = 0;
            }
            else
            {
                queryScreensReply.data.ScreenCount = Screens.Count;
                queryScreensReply.data.Screens = Screens.Select(x => new QueryScreensReplyScreen().MapFrom(x)).ToList();
            }
            queryScreensReply.SetSuccessReply();
            return queryScreensReply;
        }

        #endregion

        #region 根据影厅编码获取影厅信息

        [HttpGet]
        public QueryScreenInfoReply QueryScreenInfo(string UserName, string Password, string CinemaCode,string ScreenCode)
        {
            QueryScreenInfoReply queryScreenInfoReply = new QueryScreenInfoReply();
            //校验参数
            if (!queryScreenInfoReply.RequestInfoGuard(UserName, Password, CinemaCode, ScreenCode))
            {
                return queryScreenInfoReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryScreenInfoReply.SetUserCredentialInvalidReply();
                return queryScreenInfoReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryScreenInfoReply.SetCinemaInvalidReply();
                return queryScreenInfoReply;
            }

            var ScreenInfo = _screenInfoService.GetScreenInfo(CinemaCode, ScreenCode);

            queryScreenInfoReply.data = new QueryScreenInfoReplyScreenInfo();

            if (ScreenInfo != null)
            {
                queryScreenInfoReply.data.MapFrom(ScreenInfo);
            }
            queryScreenInfoReply.SetSuccessReply();
            return queryScreenInfoReply;
        }

        #endregion

        #region 获取影厅座位信息

        [HttpGet]
        public QueryScreenSeatsReply QueryScreenSeats(string UserName, string Password, string CinemaCode,string ScreenCode)
        {
            QueryScreenSeatsReply queryScreenSeatsReply = new QueryScreenSeatsReply();
            //校验参数
            if (!queryScreenSeatsReply.RequestInfoGuard(UserName, Password, CinemaCode, ScreenCode))
            {
                return queryScreenSeatsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryScreenSeatsReply.SetUserCredentialInvalidReply();
                return queryScreenSeatsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryScreenSeatsReply.SetCinemaInvalidReply();
                return queryScreenSeatsReply;
            }

            var Seats = _seatInfoService.GetScreenSeats(CinemaCode, ScreenCode);

            queryScreenSeatsReply.data = new QueryScreenSeatsReplySeats();
            if (Seats == null || Seats.Count == 0)
            {
                queryScreenSeatsReply.data.SeatCount = 0;
            }
            else
            {
                queryScreenSeatsReply.data.SeatCount = Seats.Count;
                queryScreenSeatsReply.data.Seats = Seats.Select(x => new QueryScreenSeatsReplySeat().MapFrom(x)).ToList();
            }
            queryScreenSeatsReply.SetSuccessReply();
            return queryScreenSeatsReply;
        }

        #endregion
    }
}