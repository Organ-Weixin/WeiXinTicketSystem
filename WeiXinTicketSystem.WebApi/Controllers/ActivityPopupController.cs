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
    public class ActivityPopupController : ApiController
    {
        ActivityPopupService _activityPopupService;
        SystemUserService _userService;
        CinemaService _cinemaService;

        #region ctor
        public ActivityPopupController()
        {
            _activityPopupService = new ActivityPopupService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
        }
        #endregion

        #region 查询活动弹窗

        [HttpGet]
        public async Task<QueryActivityPopupsReply> QueryActivityPopups(string UserName, string Password, string CinemaCode, string CurrentPage, string PageSize)
        {
            QueryActivityPopupsReply queryActivityPopupsReply = new QueryActivityPopupsReply();
            //校验参数
            if (!queryActivityPopupsReply.RequestInfoGuard(UserName, Password, CinemaCode, CurrentPage, PageSize))
            {
                return queryActivityPopupsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryActivityPopupsReply.SetUserCredentialInvalidReply();
                return queryActivityPopupsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryActivityPopupsReply.SetCinemaInvalidReply();
                return queryActivityPopupsReply;
            }
            var ActivityPopups = await _activityPopupService.QueryActivityPopupsByCinemaCodePagedAsync(CinemaCode, int.Parse(CurrentPage), int.Parse(PageSize));

            queryActivityPopupsReply.data = new QueryActivityPopupsReplyPopups();
            if (ActivityPopups == null || ActivityPopups.Count == 0)
            {
                queryActivityPopupsReply.data.ActivityPopupCount = 0;
            }
            else
            {
                queryActivityPopupsReply.data.ActivityPopupCount = ActivityPopups.Count;
                queryActivityPopupsReply.data.ActivityPopups = ActivityPopups.Select(x => new QueryActivityPopupsReplyPopup().MapFrom(x)).ToList();
            }
            queryActivityPopupsReply.SetSuccessReply();
            return queryActivityPopupsReply;
        }

        #endregion

        #region 根据推荐类型查询活动弹窗

        [HttpGet]
        public async Task<QueryActivityPopupsReply> QueryActivityPopupsByGradeCode(string UserName, string Password, string CinemaCode, string GradeCode)
        {
            QueryActivityPopupsReply queryActivityPopupReply = new QueryActivityPopupsReply();
            //校验参数
            if (!queryActivityPopupReply.RequestInfoGuard(UserName, Password, CinemaCode, GradeCode))
            {
                return queryActivityPopupReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryActivityPopupReply.SetUserCredentialInvalidReply();
                return queryActivityPopupReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryActivityPopupReply.SetCinemaInvalidReply();
                return queryActivityPopupReply;
            }
            var ActivityPopups = await _activityPopupService.QueryActivityPopupsByGradeCode(CinemaCode, GradeCode);

            queryActivityPopupReply.data = new QueryActivityPopupsReplyPopups();
            if (ActivityPopups == null || ActivityPopups.Count == 0)
            {
                queryActivityPopupReply.data.ActivityPopupCount = 0;
            }
            else
            {
                queryActivityPopupReply.data.ActivityPopupCount = ActivityPopups.Count;
                queryActivityPopupReply.data.ActivityPopups = ActivityPopups.Select(x => new QueryActivityPopupsReplyPopup().MapFrom(x)).ToList();
            }
            queryActivityPopupReply.SetSuccessReply();
            return queryActivityPopupReply;
        }


        #endregion

    }
}