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

using NetSaleSvc.Api.Models;
using NetSaleSvc.Api.Core;

namespace WeiXinTicketSystem.WebApi.Controllers
{
    public class MemberController : ApiController
    {
        MemberCardService _memberCardService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;
        private NetSaleSvcCore netSaleService = NetSaleSvcCore.Instance;

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
        public LoginCardReply LoginCard(string UserName, string Password, string CinemaCode,string OpenID, string CardNo, string CardPassword)
        {
            LoginCardReply loginCardReply = netSaleService.LoginCard(UserName, Password, CinemaCode, CardNo, CardPassword);
            //新增会员卡需要传入OpenID,之后修改就不需要再操作
            if (loginCardReply.Status == "Success")
            {
                var membercard = _memberCardService.GetMemberCardByCardNo(CinemaCode, CardNo);
                membercard.OpenID = OpenID;
                _memberCardService.Update(membercard);
            }
            return loginCardReply;
        }

        [HttpGet]
        public QueryCardReply QueryCard(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword)
        {
            return netSaleService.QueryCard(UserName, Password, CinemaCode, CardNo, CardPassword);
        }

        [HttpPost]
        public QueryDiscountReply QueryDiscount(NetSaleQueryJson QueryJson)
        {
            return netSaleService.QueryDiscount(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
        }

        [HttpGet]
        public CardPayReply CardPay(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword, string PayAmount, string SessionCode, string FilmCode, string TicketNum)
        {
            return netSaleService.CardPay(UserName, Password, CinemaCode, CardNo, CardPassword, PayAmount, SessionCode, FilmCode, TicketNum);
        }

        [HttpGet]
        public CardPayBackReply CardPayBack(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword, string TradeNo, string PayBackAmount)
        {
            return netSaleService.CardPayBack(UserName, Password, CinemaCode, CardNo, CardPassword, TradeNo, PayBackAmount);
        }

        [HttpGet]
        public QueryCardTradeRecordReply QueryCardTradeRecord(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword, string StartDate, string EndDate, string PageSize, string PageNum)
        {
            return netSaleService.QueryCardTradeRecord(UserName, Password, CinemaCode, CardNo, CardPassword, StartDate, EndDate, PageSize, PageNum);
        }

        [HttpGet]
        public CardChargeReply CardCharge(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword, string ChargeType, string ChargeAmount)
        {
            return netSaleService.CardCharge(UserName, Password, CinemaCode, CardNo, CardPassword, ChargeType, ChargeAmount);
        }

        [HttpGet]
        public QueryCardLevelReply QueryCardLevel(string UserName, string Password, string CinemaCode)
        {
            return netSaleService.QueryCardLevel(UserName, Password, CinemaCode);
        }

        [HttpGet]
        public CardRegisterReply CardRegister(string UserName, string Password, string CinemaCode, string CardPassword, string LevelCode, string InitialAmount, string CardUserName, string MobilePhone, string IDNumber, string Sex)
        {
            return netSaleService.CardRegister(UserName, Password, CinemaCode, CardPassword, LevelCode, InitialAmount, CardUserName, MobilePhone, IDNumber, Sex);
        }

        #region 根据手机号码查询会员卡信息

        [HttpGet]
        public QueryMemberPhoneReply QueryMemberCardByPhone(string UserName, string Password, string CinemaCode, string MobilePhone)
        {
            QueryMemberPhoneReply queryMemberPhoneReply = new QueryMemberPhoneReply();
            //校验参数
            if (!queryMemberPhoneReply.RequestInfoGuard(UserName, Password, CinemaCode, MobilePhone))
            {
                return queryMemberPhoneReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryMemberPhoneReply.SetUserCredentialInvalidReply();
                return queryMemberPhoneReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryMemberPhoneReply.SetCinemaInvalidReply();
                return queryMemberPhoneReply;
            }

            var MemberPhone = _memberCardService.GetMemberCardByMobilePhoneAsync(CinemaCode, MobilePhone);

            queryMemberPhoneReply.data = new QueryMemberPhoneReplyPhones();
            if (MemberPhone == null || MemberPhone.Count == 0)
            {
                queryMemberPhoneReply.data.MemberPhoneCount = 0;
            }
            else
            {
                queryMemberPhoneReply.data.MemberPhoneCount = MemberPhone.Count;
                queryMemberPhoneReply.data.MemberPhones = MemberPhone.Select(x => new QueryMemberPhoneReplyPhone().MapFrom(x)).ToList();
            }
            queryMemberPhoneReply.SetSuccessReply();
            return queryMemberPhoneReply;
        }

        #endregion
    }
}