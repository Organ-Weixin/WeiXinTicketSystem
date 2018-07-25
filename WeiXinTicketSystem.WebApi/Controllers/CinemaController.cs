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
    public class CinemaController : ApiController
    {
        CinemaService _cinemaService;
        SystemUserService _userService;

        #region ctor
        public CinemaController()
        {
            _cinemaService = new CinemaService();
            _userService = new SystemUserService();
        }
        #endregion


        #region 获取影院信息

        [HttpGet]
        public async Task<QueryCinemasReply> QueryCinemas(string UserName, string Password, string CinemaCode, string CurrentPage, string PageSize)
        {
            QueryCinemasReply queryCinemasReply = new QueryCinemasReply();
            //校验参数
            if (!queryCinemasReply.RequestInfoGuard(UserName, Password, CinemaCode, CurrentPage, PageSize))
            {
                return queryCinemasReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryCinemasReply.SetUserCredentialInvalidReply();
                return queryCinemasReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryCinemasReply.SetCinemaInvalidReply();
                return queryCinemasReply;
            }

            var cinemas = await _cinemaService.QueryCinemasPagedAsync(int.Parse(CurrentPage), int.Parse(PageSize));

            queryCinemasReply.data = new QueryCinemasReplyCinemas();
            if (cinemas == null || cinemas.Count == 0)
            {
                queryCinemasReply.data.CinemaCount = 0;
            }
            else
            {
                queryCinemasReply.data.CinemaCount = cinemas.Count;
                queryCinemasReply.data.Cinemas = cinemas.Select(x => new QueryCinemasReplyCinema().MapFrom(x)).ToList();
            }
            queryCinemasReply.SetSuccessReply();
            return queryCinemasReply;
        }

        #endregion

    }
}