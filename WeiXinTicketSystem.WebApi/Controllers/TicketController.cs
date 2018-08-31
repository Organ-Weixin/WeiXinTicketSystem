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
    public class TicketController : ApiController
    {
        #region 私有变量
        /// <summary>
        /// 服务引用
        /// </summary>
        private NetSaleSvcCore netSaleService = NetSaleSvcCore.Instance;
        OrderService _orderService = new OrderService();
        UserInfoService _userInfoService = new UserInfoService();
        CinemaService _cinemaService = new CinemaService();
        UserCinemaService _userCinemaService = new UserCinemaService();
        MemberCardService _memberCardService = new MemberCardService();
        #endregion

        [HttpPost]
        public LockSeatReply LockSeat(NetSaleQueryJson QueryJson)
        {
            LockSeatReply lockSeatReply = netSaleService.LockSeat(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
            //锁座时新增订单需要传入OpenID,之后修改订单就不需要再操作
            if (lockSeatReply.Status == "Success")
            {
                var orderbase = _orderService.GetOrderBaseInfoByLockOrderCode(lockSeatReply.Order.OrderCode);
                orderbase.OpenID = QueryJson.OpenID;
                _orderService.UpdateOrderBaseInfo(orderbase);
            }
            return lockSeatReply;
        }

        [HttpPost]
        public ReleaseSeatReply ReleaseSeat(NetSaleQueryJson QueryJson)
        {
            return netSaleService.ReleaseSeat(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
        }

        [HttpPost]
        public SubmitOrderReply SubmitOrder(NetSaleQueryJson QueryJson)
        {
            return netSaleService.SubmitOrder(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
        }

        /// <summary>
        /// 1905的会员卡支付提交另外实现
        /// </summary>
        /// <param name="QueryJson"></param>
        /// <returns></returns>
        [HttpPost]
        public SubmitOrderReply SubmitOrder_1905CardPay(SubmitOrder_1905CardPayQueryJson QueryJson)
        {
            SubmitOrderReply submitOrderReply = new SubmitOrderReply();
            //校验参数
            if (!submitOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.CardNo, QueryJson.CardPassword, QueryJson.OrderCode, QueryJson.LowestPrice.ToString(), QueryJson.Seats))
            {
                return submitOrderReply;
            }
            //获取用户信息(渠道)
            UserInfoEntity UserInfo = _userInfoService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                submitOrderReply.SetUserCredentialInvalidReply();
                return submitOrderReply;
            }
            //验证影院是否存在且可访问(包含接口账号信息)
            var userCinema = _userCinemaService.GetUserCinema(UserInfo.Id, QueryJson.CinemaCode);
            if (userCinema == null)
            {
                submitOrderReply.SetCinemaInvalidReply();
                return submitOrderReply;
            }
            //验证会员卡是否存在
            var memberCard = _memberCardService.GetMemberCardByCardNo(QueryJson.CinemaCode, QueryJson.CardNo);
            if (memberCard == null)
            {
                submitOrderReply.SetMemberCardInvalidReply();
                return submitOrderReply;
            }
            //验证订单是否存在
            OrderViewEntity order = null;
            if (!string.IsNullOrEmpty(QueryJson.OrderCode))
            {
                order = _orderService.GetOrderWithLockOrderCode(QueryJson.CinemaCode
                    , QueryJson.OrderCode);
            }
            if (order == null
                || (order.orderBaseInfo.OrderStatus != OrderStatusEnum.Locked
                && order.orderBaseInfo.OrderStatus != OrderStatusEnum.SubmitFail))
            {
                submitOrderReply.SetOrderNotExistReply();
                return submitOrderReply;
            }
            //验证座位数量
            if (QueryJson.Seats.Count != order.orderBaseInfo.TicketCount)
            {
                submitOrderReply.SetSeatCountInvalidReply();
                return submitOrderReply;
            }
            //更新订单信息
            order.MapFrom(QueryJson);
            //开始调用1905接口
            string pSeatNo = string.Join(",", order.orderSeatDetails.Select(x => x.SeatCode));
            string pMemberPrice = string.Join(",", order.orderSeatDetails.Select(x => x.SalePrice));
            string pFee = string.Join(",", order.orderSeatDetails.Select(x => x.Fee));
            string pVerifyInfo = MD5Helper.MD5Encrypt(userCinema.RealUserName + QueryJson.CardNo + QueryJson.CardPassword + pFee + QueryJson.LowestPrice.ToString("0.##") + pMemberPrice + order.orderBaseInfo.LockOrderCode + pSeatNo  + userCinema.RealPassword).ToLower();
            SortedDictionary<string, string> paramDic = new SortedDictionary<string, string>
            {
                { "pAppCode", userCinema.RealUserName },
                { "pCardNo",QueryJson.CardNo },
                { "pCardPwd",QueryJson.CardPassword },
                { "pFee",pFee },
                { "pLowestPrice",QueryJson.LowestPrice.ToString("0.##") },
                { "pMemberPrice",pMemberPrice },
                { "pOrderID", order.orderBaseInfo.LockOrderCode },
                { "pSeatNo",pSeatNo },
                { "pVerifyInfo", pVerifyInfo }
            };
            string SellTicketCustomResult = HttpHelper.VisitUrl(userCinema.Url + "/SellTicketCustom/member", FormatParam(paramDic));
            Dy1905SellTicketCustomReply Dy1905Reply = SellTicketCustomResult.Deserialize<Dy1905SellTicketCustomReply>();
            if (Dy1905Reply.ResultCode == "0")
            {
                //更新订单信息
                order.orderBaseInfo.SubmitOrderCode = Dy1905Reply.OrderNo;
                order.orderBaseInfo.PrintNo = Dy1905Reply.PrintNo;
                order.orderBaseInfo.VerifyCode = Dy1905Reply.VerifyCode;
                //同时更新订单支付信息，会员卡支付和提交是一起的
                order.orderBaseInfo.PayFlag = 1;
                order.orderBaseInfo.OrderPayType = PayTypeEnum.MemberCardPay;
                order.orderBaseInfo.PayTime = DateTime.Now;
                order.orderBaseInfo.CardNo = QueryJson.CardNo;

                order.orderBaseInfo.OrderStatus = OrderStatusEnum.Submited;
                order.orderBaseInfo.SubmitTime = DateTime.Now;

                //准备返回数据
                submitOrderReply.Order = new SubmitOrderReplyOrder();
                submitOrderReply.Order.CinemaType = (NetSaleSvc.Entity.Enum.CinemaTypeEnum)userCinema.CinemaType;
                submitOrderReply.Order.OrderCode = order.orderBaseInfo.SubmitOrderCode;
                submitOrderReply.Order.SessionCode = order.orderBaseInfo.SessionCode;
                submitOrderReply.Order.Count = order.orderBaseInfo.TicketCount;
                submitOrderReply.Order.PrintNo = order.orderBaseInfo.PrintNo;
                submitOrderReply.Order.VerifyCode = order.orderBaseInfo.VerifyCode;
                submitOrderReply.Order.Seat = order.orderSeatDetails.Select(x =>
                    new SubmitOrderReplySeat
                    {
                        SeatCode = x.SeatCode,
                        FilmTicketCode = x.FilmTicketCode
                    }).ToList();

                submitOrderReply.SetSuccessReply();
            }
            else
            {
                order.orderBaseInfo.OrderStatus = OrderStatusEnum.SubmitFail;
                order.orderBaseInfo.ErrorMessage = Dy1905Reply.ResultCode;

                submitOrderReply.Status = "Failure";
                submitOrderReply.ErrorCode = Dy1905Reply.ResultCode;
            }
            //更新订单信息
            _orderService.Update(order);

            return submitOrderReply;
        }

        [HttpGet]
        public RefundTicketReply RefundTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.RefundTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public QueryOrderReply QueryOrder(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            return netSaleService.QueryOrder(UserName, Password, CinemaCode, OrderCode);
        }

        [HttpGet]
        public QueryTicketReply QueryTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.QueryTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public QueryPrintReply QueryPrint(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.QueryPrint(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public FetchTicketReply FetchTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.FetchTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        /// <summary>
        /// 参数格式化
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string FormatParam(SortedDictionary<string, string> param)
        {
            string queryParams = "";
            foreach (var key in param.Keys)
            {
                string value = param[key];
                if (value != null)
                    queryParams += key + "=" + value.ToString() + "&";

            }
            if (queryParams.Length > 0)
                queryParams = queryParams.Substring(0, queryParams.Length - 1); //remove last '&'
            return queryParams;
        }
    }
}
