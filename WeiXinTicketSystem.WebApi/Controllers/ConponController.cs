﻿using System;
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

        #region ctor
        public ConponController()
        {
            _conponService = new ConponService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
        }
        #endregion

        [HttpGet]
        public async Task<QueryConponsReply> QueryConpons(string UserName, string Password, string CinemaCode, string OpenID, string statusID, string CurrentPage, string PageSize)
        {
            QueryConponsReply queryConponsReply = new QueryConponsReply();
            ////校验参数
            //if (!queryConponsReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID, statusID,CurrentPage, PageSize))
            //{
            //    return queryConponsReply;
            //}
            ////获取用户信息
            //SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            //if (UserInfo == null)
            //{
            //    queryConponsReply.SetUserCredentialInvalidReply();
            //    return queryConponsReply;
            //}
            ////验证影院是否存在且可访问
            //var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            //if (cinema == null)
            //{
            //    queryConponsReply.SetCinemaInvalidReply();
            //    return queryConponsReply;
            //}
            ////验证用户OpenId是否存在
            //var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            //if (ticketuser == null)
            //{
            //    queryConponsReply.SetOpenIDNotExistReply();
            //    return queryConponsReply;
            //}
            //var Snacks = await _conponService.QuerySnacksPagedAsync(CinemaCode, TypeId, int.Parse(CurrentPage), int.Parse(PageSize));

            //querySnacksReply.data = new QuerySnacksReplySnacks();
            //if (Snacks == null || Snacks.Count == 0)
            //{
            //    querySnacksReply.data.SnackCount = 0;
            //}
            //else
            //{
            //    querySnacksReply.data.SnackCount = Snacks.Count;
            //    querySnacksReply.data.Snacks = Snacks.Select(x => new QuerySnacksReplySnack().MapFrom(x)).ToList();
            //}
            //querySnacksReply.SetSuccessReply();
            return queryConponsReply;
        }
    }
}