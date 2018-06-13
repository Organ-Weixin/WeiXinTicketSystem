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
    }
}