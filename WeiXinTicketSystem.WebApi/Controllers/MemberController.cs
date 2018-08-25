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
        public LoginCardReply LoginCard(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword)
        {
            return netSaleService.LoginCard(UserName, Password, CinemaCode, CardNo, CardPassword);
        }

        [HttpGet]
        public QueryCardReply QueryCard(string UserName, string Password, string CinemaCode, string CardNo, string CardPassword)
        {
            return netSaleService.QueryCard(UserName, Password, CinemaCode, CardNo, CardPassword);
        }

        [HttpPost]
        public QueryDiscountReply QueryDiscount(string UserName, string Password, string QueryXml)
        {
            return netSaleService.QueryDiscount(UserName,Password,QueryXml);
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
    }
}