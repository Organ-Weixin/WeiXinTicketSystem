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
    public class ConponController : ApiController
    {
        ConponService _conponService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;
        GivingConditionsService _givingConditionsService;
        MemberChargeSettingService _memberChargeSettingService;

        #region ctor
        public ConponController()
        {
            _conponService = new ConponService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
            _givingConditionsService = new GivingConditionsService();
            _memberChargeSettingService = new MemberChargeSettingService();
        }
        #endregion

        #region 获取用户优惠券(分页)
        [HttpGet]
        public async Task<QueryConponsReply> QueryUserConponsPaged(string UserName, string Password, string CinemaCode, string OpenID, string Status, string CurrentPage, string PageSize)
        {
            QueryConponsReply queryConponsReply = new QueryConponsReply();
            //校验参数
            if (!queryConponsReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID, Status, CurrentPage, PageSize))
            {
                return queryConponsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryConponsReply.SetUserCredentialInvalidReply();
                return queryConponsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryConponsReply.SetCinemaInvalidReply();
                return queryConponsReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryConponsReply.SetOpenIDNotExistReply();
                return queryConponsReply;
            }
            //验证优惠券状态
            var StatusEnum = Status.CastToEnum<ConponStatusEnum>();
            if (StatusEnum == default(ConponStatusEnum))
            {
                queryConponsReply.SetConponStatusInvalidReply();
                return queryConponsReply;
            }
            var Conpons = await _conponService.QueryConponsPagedAsync(CinemaCode, OpenID, StatusEnum, int.Parse(CurrentPage), int.Parse(PageSize));

            queryConponsReply.data = new QueryConponsReplyConpons();
            if (Conpons == null || Conpons.Count == 0)
            {
                queryConponsReply.data.ConponCount = 0;
            }
            else
            {
                queryConponsReply.data.ConponCount = Conpons.Count;
                queryConponsReply.data.Conpons = Conpons.Select(x => new QueryConponsReplyConpon().MapFrom(x)).ToList();
            }
            queryConponsReply.SetSuccessReply();
            return queryConponsReply;
        }
        #endregion

        #region 获取用户优惠券(不分页)

        [HttpGet]
        public async Task<QueryConponsReply> QueryUserConpons(string UserName, string Password, string CinemaCode, string OpenID, string Status)
        {
            QueryConponsReply queryConponsReply = new QueryConponsReply();
            //校验参数
            if (!queryConponsReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID, Status))
            {
                return queryConponsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryConponsReply.SetUserCredentialInvalidReply();
                return queryConponsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryConponsReply.SetCinemaInvalidReply();
                return queryConponsReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryConponsReply.SetOpenIDNotExistReply();
                return queryConponsReply;
            }
            //验证优惠券状态
            var StatusEnum = Status.CastToEnum<ConponStatusEnum>();
            if (StatusEnum == default(ConponStatusEnum))
            {
                queryConponsReply.SetConponStatusInvalidReply();
                return queryConponsReply;
            }
            var Conpons = await _conponService.QueryConponsAsync(CinemaCode, OpenID, StatusEnum);

            queryConponsReply.data = new QueryConponsReplyConpons();
            if (Conpons == null || Conpons.Count == 0)
            {
                queryConponsReply.data.ConponCount = 0;
            }
            else
            {
                queryConponsReply.data.ConponCount = Conpons.Count;
                queryConponsReply.data.Conpons = Conpons.Select(x => new QueryConponsReplyConpon().MapFrom(x)).ToList();
            }
            queryConponsReply.SetSuccessReply();
            return queryConponsReply;
        }

        #endregion

        #region 赠送优惠券
        [HttpPost]
        public async Task<SendConponReply> SendConpon(SendConponQueryJson QueryJson)
        {
            SendConponReply sendConponReply = new SendConponReply();
            //校验参数
            if (!sendConponReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.GroupCode, QueryJson.Number.ToString(), QueryJson.OpenID))
            {
                return sendConponReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                sendConponReply.SetUserCredentialInvalidReply();
                return sendConponReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                sendConponReply.SetCinemaInvalidReply();
                return sendConponReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                sendConponReply.SetOpenIDNotExistReply();
                return sendConponReply;
            }


            List<ConponEntity> conpons = new List<ConponEntity>();
            IList<ConponEntity> iconpons = _conponService.GetConponByTypeCodeAsync(QueryJson.CinemaCode, QueryJson.GroupCode);
            if (iconpons.Count < QueryJson.Number)
            {
                sendConponReply.SetCouponNumberLessThanReply();
                return sendConponReply;
            }
            var iconpons2 = iconpons.OrderBy(x => Guid.NewGuid()).Take(QueryJson.Number);
            conpons.AddRange(iconpons2);
            foreach (ConponEntity conpon in conpons)
            {
                conpon.OpenID = QueryJson.OpenID;
                conpon.ReceivedDate = DateTime.Now;
                conpon.Status = ConponStatusEnum.AlreadyReceived;
                await _conponService.UpdateAsync(conpon);
            }

            sendConponReply.data = new SendConponReplyConpons();
            if (conpons == null || conpons.Count == 0)
            {
                sendConponReply.data.ConponCount = 0;
            }
            else
            {
                sendConponReply.data.ConponCount = conpons.Count;
                sendConponReply.data.Conpons = conpons.Select(x => new SendConponReplyConpon().MapFrom(x)).ToList();
            }

            sendConponReply.SetSuccessReply();
            return sendConponReply;
        }
        #endregion

        #region 查询该影院下所有赠送条件

        [HttpGet]
        public async Task<QueryGivingConditionsReply> QueryGivingConditions(string UserName, string Password, string CinemaCode)
        {
            QueryGivingConditionsReply queryGivingConditionsReply = new QueryGivingConditionsReply();
            //校验参数
            if (!queryGivingConditionsReply.RequestInfoGuard(UserName, Password, CinemaCode))
            {
                return queryGivingConditionsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryGivingConditionsReply.SetUserCredentialInvalidReply();
                return queryGivingConditionsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryGivingConditionsReply.SetCinemaInvalidReply();
                return queryGivingConditionsReply;
            }
            
            var givingConditions = await _givingConditionsService.GetGivingConditionViewByCinemaCodeAsync(CinemaCode);

            queryGivingConditionsReply.data = new QueryGivingConditionsReplyConditions();
            if (givingConditions == null || givingConditions.Count == 0)
            {
                queryGivingConditionsReply.data.ConditionsCount = 0;
            }
            else
            {
                queryGivingConditionsReply.data.ConditionsCount = givingConditions.Count;
                queryGivingConditionsReply.data.Conditions = givingConditions.Select(x => new QueryGivingConditionsReplyCondition().MapFrom(x)).ToList();
            }
            queryGivingConditionsReply.SetSuccessReply();
            return queryGivingConditionsReply;
        }


        #endregion


        #region 查询该影院下所有会员卡充值赠送条件

        [HttpGet]
        public async Task<QueryMemberChargeSettingReply> QueryMemberChargeSettings(string UserName, string Password, string CinemaCode)
        {
            QueryMemberChargeSettingReply queryMemberChargeSettingsReply = new QueryMemberChargeSettingReply();
            //校验参数
            if (!queryMemberChargeSettingsReply.RequestInfoGuard(UserName, Password, CinemaCode))
            {
                return queryMemberChargeSettingsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryMemberChargeSettingsReply.SetUserCredentialInvalidReply();
                return queryMemberChargeSettingsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryMemberChargeSettingsReply.SetCinemaInvalidReply();
                return queryMemberChargeSettingsReply;
            }

            var memberChargeSetting = await _memberChargeSettingService.GetMemberChargeSettingViewByCinemaCodeAsync(CinemaCode);

            queryMemberChargeSettingsReply.data = new QueryMemberChargeSettingReplySettings();
            if (memberChargeSetting == null || memberChargeSetting.Count == 0)
            {
                queryMemberChargeSettingsReply.data.MemberChargeSettingCount = 0;
            }
            else
            {
                queryMemberChargeSettingsReply.data.MemberChargeSettingCount = memberChargeSetting.Count;
                queryMemberChargeSettingsReply.data.MemberChargeSettings = memberChargeSetting.Select(x => new QueryMemberChargeSettingReplySetting().MapFrom(x)).ToList();
            }
            queryMemberChargeSettingsReply.SetSuccessReply();
            return queryMemberChargeSettingsReply;
        }


        #endregion

    }
}