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

        #region ctor
        public ScreenController()
        {
            _screenInfoService = new ScreenInfoService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
        }
        #endregion

        #region 获取影厅信息

        [HttpGet]
        public QueryScreensReply QueryFilmComments(string UserName, string Password, string CinemaCode)
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
    }
}