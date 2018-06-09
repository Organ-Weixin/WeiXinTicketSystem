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
    public class ActivityController : ApiController
    {
        ActivityService _activityService;
        SystemUserService _userService;
        CinemaService _cinemaService;

        #region ctor
        public ActivityController()
        {
            _activityService = new ActivityService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
        }
        #endregion


        [HttpGet]
        public async Task<QueryActivityReply> QueryActivitys(string UserName, string Password, string CinemaCode, string CurrentPage, string PageSize)
        {
            QueryActivityReply queryActivityReply = new QueryActivityReply();
            //校验参数
            if (!queryActivityReply.RequestInfoGuard(UserName, Password, CinemaCode, CurrentPage, PageSize))
            {
                return queryActivityReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryActivityReply.SetUserCredentialInvalidReply();
                return queryActivityReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryActivityReply.SetCinemaInvalidReply();
                return queryActivityReply;
            }
            var Activitys = await _activityService.QueryActivitysPagedAsync(CinemaCode, int.Parse(CurrentPage), int.Parse(PageSize));

            queryActivityReply.data = new QueryActivityReplyActivitys();
            if (Activitys == null || Activitys.Count == 0)
            {
                queryActivityReply.data.ActivityCount = 0;
            }
            else
            {
                queryActivityReply.data.ActivityCount = Activitys.Count;
                queryActivityReply.data.Activitys = Activitys.Select(x => new QueryActivityReplyActivity().MapFrom(x)).ToList();
            }
            queryActivityReply.SetSuccessReply();
            return queryActivityReply;
        }
    }
}