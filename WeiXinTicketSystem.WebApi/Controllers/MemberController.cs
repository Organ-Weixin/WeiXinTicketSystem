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
    public class MemberController : ApiController
    {
        MemberCardService _memberCardService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;

        #region ctor
        public MemberController()
        {
            _memberCardService = new MemberCardService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
        }
        #endregion


        #region 查询会员信息
        [HttpGet]
        public async Task<QueryMembersReply> QueryMember(string UserName, string Password, string CinemaCode, string OpenID)
        {
            QueryMembersReply queryMembersReply = new QueryMembersReply();

            //校验参数
            if (!queryMembersReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID))
            {
                return queryMembersReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryMembersReply.SetUserCredentialInvalidReply();
                return queryMembersReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryMembersReply.SetCinemaInvalidReply();
                return queryMembersReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryMembersReply.SetOpenIDNotExistReply();
                return queryMembersReply;
            }
            var Members =await _memberCardService.GetMemberCardByOpenIDAsync(CinemaCode, OpenID);
            queryMembersReply.data = new QueryMembersReplyMembers();
            if (Members == null || Members.Count == 0)
            {
                queryMembersReply.data.MemberCount = 0;
            }
            else
            {
                queryMembersReply.data.MemberCount = Members.Count;
                queryMembersReply.data.Members = Members.Select(x => new QueryMembersReplyMember().MapFrom(x)).ToList();
            }
            queryMembersReply.SetSuccessReply();
            return queryMembersReply;
        }

        #endregion

        #region 开通会员
        [HttpPost]
        public RegisterMemberReply RegisterMember(RegisterMemberQueryJson QueryJson)
        {
            RegisterMemberReply registerMemberReply = new RegisterMemberReply();
            //校验参数
            if (!registerMemberReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OpenID, QueryJson.CardNo, QueryJson.CardPassword, QueryJson.Balance.ToString(), QueryJson.Score.ToString(),QueryJson.MemberGrade.ToString()))
            {
                return registerMemberReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                registerMemberReply.SetUserCredentialInvalidReply();
                return registerMemberReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                registerMemberReply.SetCinemaInvalidReply();
                return registerMemberReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                registerMemberReply.SetOpenIDNotExistReply();
                return registerMemberReply;
            }

            
            //将请求参数转为会员卡信息
            var memberCard = new MemberCardEntity();
            memberCard.MapFrom(QueryJson);


            _memberCardService.Insert(memberCard);

            registerMemberReply.data = new RegisterMemberReplyMember();
            registerMemberReply.data.MapFrom(memberCard);
            registerMemberReply.SetSuccessReply();

            return registerMemberReply;

        }
        #endregion
    }
}