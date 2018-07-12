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
    public class TicketController : ApiController
    {
        OrderService _orderService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        SessionInfoService _sessionInfoService;
        SeatInfoService _seatInfoService;
        TicketUsersService _ticketUserService;
        ScreenInfoService _screenInfoService;
        #region ctor
        public TicketController()
        {
            _orderService = new OrderService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _sessionInfoService = new SessionInfoService();
            _seatInfoService = new SeatInfoService();
            _ticketUserService = new TicketUsersService();
            _screenInfoService = new ScreenInfoService();
        }
        #endregion

        #region 锁座
        [HttpPost]
        public LockSeatReply LockSeat(LockSeatQueryJson QueryJson)
        {
            LockSeatReply lockSeatReply = new LockSeatReply();
            //校验参数
            if (!lockSeatReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OpenID, QueryJson.Order.SessionCode, QueryJson.Order.SeatsCount.ToString(), QueryJson.Order.PayType, QueryJson.Order.Seats))
            {
                return lockSeatReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                lockSeatReply.SetUserCredentialInvalidReply();
                return lockSeatReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                lockSeatReply.SetCinemaInvalidReply();
                return lockSeatReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                lockSeatReply.SetOpenIDNotExistReply();
                return lockSeatReply;
            }
            //验证排期是否存在
            var sessionInfo = _sessionInfoService.GetSessionInfo(QueryJson.CinemaCode, QueryJson.Order.SessionCode);
            if (sessionInfo == null)
            {
                lockSeatReply.SetSessionInvalidReply();
                return lockSeatReply;
            }
            //验证座位数量
            if (QueryJson.Order.SeatsCount != QueryJson.Order.Seats.Count)
            {
                lockSeatReply.SetSeatCountInvalidReply();
                return lockSeatReply;
            }

            //将请求参数转为订单
            OrderViewEntity order = new OrderViewEntity();
            order.MapFrom(cinema, QueryJson, sessionInfo);
            var seatInfos = _seatInfoService.GetSeats(cinema.CinemaCode, order.orderSeatDetails.Select(x => x.SeatCode), sessionInfo.ScreenCode);
            order.orderSeatDetails.ForEach(x =>
            {
                var seatInfo = seatInfos.Where(y => y.SeatCode == x.SeatCode).SingleOrDefault();
                if (seatInfo != null)
                {
                    x.RowNum = seatInfo.RowNum;
                    x.ColumnNum = seatInfo.ColumnNum;
                }
            });

            NetSaleSvc.Api.Models.LockSeatReply apiLockSeatReply = _orderService.LockSeat(order);
            lockSeatReply.data = new LockSeatReplyOrder();
            if (apiLockSeatReply.Status == "Success")
            {
                lockSeatReply.data.OrderCode = order.orderBaseInfo.LockOrderCode;
                lockSeatReply.data.AutoUnlockDatetime = order.orderBaseInfo.AutoUnlockDatetime
                    .GetValueOrDefault(DateTime.Now.AddMinutes(10)).ToFormatStringWithT();
                lockSeatReply.data.SessionCode = order.orderBaseInfo.SessionCode;
                lockSeatReply.data.SeatsCount = order.orderBaseInfo.TicketCount;
                lockSeatReply.data.Seats = order.orderSeatDetails.Select(x => new LockSeatReplySeat { SeatCode = x.SeatCode }).ToList();

                lockSeatReply.SetSuccessReply();
            }
            else
            {
                lockSeatReply.GetErrorFromNetSaleReply(apiLockSeatReply);
            }
            return lockSeatReply;

        }
        #endregion

        #region 解锁座位
        [HttpPost]
        public ReleaseSeatReply ReleaseSeat(ReleaseSeatQueryJson QueryJson)
        {
            ReleaseSeatReply releaseSeatReply = new ReleaseSeatReply();
            //校验参数
            if (!releaseSeatReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.Order.OrderCode, QueryJson.Order.SessionCode, QueryJson.Order.SeatsCount.ToString(), QueryJson.Order.Seats))
            {
                return releaseSeatReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                releaseSeatReply.SetUserCredentialInvalidReply();
                return releaseSeatReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                releaseSeatReply.SetCinemaInvalidReply();
                return releaseSeatReply;
            }
            //验证座位数量
            if (QueryJson.Order.SeatsCount != QueryJson.Order.Seats.Count)
            {
                releaseSeatReply.SetSeatCountInvalidReply();
                return releaseSeatReply;
            }
            //验证订单是否存在
            OrderViewEntity order = null;
            if (!string.IsNullOrEmpty(QueryJson.Order.OrderCode))
            {
                order = _orderService.GetOrderWithLockOrderCode(QueryJson.CinemaCode
                    , QueryJson.Order.OrderCode);
            }
            if (order == null
                || (order.orderBaseInfo.OrderStatus != OrderStatusEnum.Locked
                && order.orderBaseInfo.OrderStatus != OrderStatusEnum.SubmitFail
                && order.orderBaseInfo.OrderStatus != OrderStatusEnum.ReleaseFail))
            {
                releaseSeatReply.SetOrderNotExistReply();
                return releaseSeatReply;
            }

            NetSaleSvc.Api.Models.ReleaseSeatReply apiReleaseSeatReply = _orderService.ReleaseSeat(order);
            releaseSeatReply.data = new ReleaseSeatReplyOrder();
            if (apiReleaseSeatReply.Status == "Success")
            {
                releaseSeatReply.data.OrderCode = order.orderBaseInfo.LockOrderCode;
                releaseSeatReply.data.SessionCode = order.orderBaseInfo.SessionCode;
                releaseSeatReply.data.SeatsCount = order.orderBaseInfo.TicketCount;
                releaseSeatReply.data.Seats = order.orderSeatDetails.Select(x => new ReleaseSeatReplySeat { SeatCode = x.SeatCode }).ToList();
                releaseSeatReply.SetSuccessReply();
            }
            else
            {
                releaseSeatReply.GetErrorFromNetSaleReply(apiReleaseSeatReply);
            }
            return releaseSeatReply;
        }
        #endregion

        #region 提交订单
        [HttpPost]
        public SubmitOrderReply SubmitOrder(SubmitOrderQueryJson QueryJson)
        {
            SubmitOrderReply submitOrderReply = new SubmitOrderReply();
            //校验参数
            if (!submitOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.Order.PaySeqNo, QueryJson.Order.OrderCode, QueryJson.Order.SessionCode, QueryJson.Order.SeatsCount.ToString(), QueryJson.Order.MobilePhone, QueryJson.Order.Seats))
            {
                return submitOrderReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                submitOrderReply.SetUserCredentialInvalidReply();
                return submitOrderReply;
            }
            //验证是否传递手机号
            if (string.IsNullOrEmpty(QueryJson.Order.MobilePhone))
            {
                submitOrderReply.SetNecessaryParamMissReply("MobilePhone");
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                submitOrderReply.SetCinemaInvalidReply();
                return submitOrderReply;
            }
            //验证订单是否存在
            OrderViewEntity order = null;
            if (!string.IsNullOrEmpty(QueryJson.Order.OrderCode))
            {
                order = _orderService.GetOrderWithLockOrderCode(QueryJson.CinemaCode
                    , QueryJson.Order.OrderCode);
            }
            if (order == null
                || (order.orderBaseInfo.OrderStatus != OrderStatusEnum.Locked
                && order.orderBaseInfo.OrderStatus != OrderStatusEnum.SubmitFail))
            {
                submitOrderReply.SetOrderNotExistReply();
                return submitOrderReply;
            }
            //验证座位数量
            if (QueryJson.Order.SeatsCount != QueryJson.Order.Seats.Count || QueryJson.Order.SeatsCount != order.orderBaseInfo.TicketCount)
            {
                submitOrderReply.SetSeatCountInvalidReply();
                return submitOrderReply;
            }

            //更新订单信息
            order.MapFrom(QueryJson);

            //TODO:满天星的订单属于会员卡支付的话暂时要求传入会员卡交易流水号
            if (cinema.TicketSystem == CinemaTypeEnum.ManTianXing
                && order.orderBaseInfo.IsMemberPay
                && string.IsNullOrEmpty(order.orderBaseInfo.PaySeqNo))
            {
                submitOrderReply.SetMemberPaySeqNoNotExistReply();
                return submitOrderReply;
            }

            NetSaleSvc.Api.Models.SubmitOrderReply apiSubmitOrderReply = _orderService.SubmitOrder(order);
            submitOrderReply.data = new SubmitOrderReplyOrder();
            if (apiSubmitOrderReply.Status == "Success")
            {
                submitOrderReply.data.CinemaType = cinema.TicketSystem;
                submitOrderReply.data.OrderCode = order.orderBaseInfo.LockOrderCode;
                submitOrderReply.data.SessionCode = order.orderBaseInfo.SessionCode;
                submitOrderReply.data.SeatsCount = order.orderBaseInfo.TicketCount;
                submitOrderReply.data.PrintNo = order.orderBaseInfo.PrintNo;
                submitOrderReply.data.VerifyCode = order.orderBaseInfo.VerifyCode;
                submitOrderReply.data.Seats = order.orderSeatDetails.Select(x => new SubmitOrderReplySeat { SeatCode = x.SeatCode, FilmTicketCode = x.FilmTicketCode }).ToList();
                submitOrderReply.SetSuccessReply();
            }
            else
            {
                submitOrderReply.GetErrorFromNetSaleReply(apiSubmitOrderReply);
            }
            return submitOrderReply;
        }
        #endregion

        #region 退票
        [HttpGet]
        public RefundTicketReply RefundTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            RefundTicketReply refundTicketReply = new RefundTicketReply();
            //校验参数
            if (!refundTicketReply.RequestInfoGuard(UserName, Password, CinemaCode, PrintNo, VerifyCode))
            {
                return refundTicketReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                refundTicketReply.SetUserCredentialInvalidReply();
                return refundTicketReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                refundTicketReply.SetCinemaInvalidReply();
                return refundTicketReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithPrintNo(CinemaCode, PrintNo, VerifyCode);
            if (order == null || order.orderBaseInfo.OrderStatus != OrderStatusEnum.Complete)
            {
                refundTicketReply.SetOrderNotExistReply();
                return refundTicketReply;
            }

            NetSaleSvc.Api.Models.RefundTicketReply apiRefundTicketReply = _orderService.RefundTicket(order);
            refundTicketReply.data = new RefundTicketReplyOrder();
            if (apiRefundTicketReply.Status == "Success")
            {
                refundTicketReply.data.OrderCode = order.orderBaseInfo.SubmitOrderCode;
                refundTicketReply.data.PrintNo = order.orderBaseInfo.PrintNo;
                refundTicketReply.data.VerifyCode = order.orderBaseInfo.VerifyCode;
                refundTicketReply.data.Status = order.orderBaseInfo.OrderStatus == OrderStatusEnum.Refund ? YesOrNoEnum.Yes : YesOrNoEnum.No;
                refundTicketReply.data.RefundTime = order.orderBaseInfo.RefundTime.ToFormatStringWithT();
                refundTicketReply.SetSuccessReply();
            }
            else
            {
                refundTicketReply.GetErrorFromNetSaleReply(apiRefundTicketReply);
            }
            return refundTicketReply;
        }
        #endregion

        #region 查询订单信息
        [HttpGet]
        public QueryOrderReply QueryOrder(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            QueryOrderReply queryOrderReply = new QueryOrderReply();
            //校验参数
            if (!queryOrderReply.RequestInfoGuard(UserName, Password, CinemaCode, OrderCode))
            {
                return queryOrderReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryOrderReply.SetUserCredentialInvalidReply();
                return queryOrderReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryOrderReply.SetCinemaInvalidReply();
                return queryOrderReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithOrderCode(CinemaCode, OrderCode);
            if (order == null)
            {
                queryOrderReply.SetOrderNotExistReply();
                return queryOrderReply;
            }
            NetSaleSvc.Api.Models.QueryOrderReply apiQueryOrderReply = _orderService.QueryOrder(order);
            queryOrderReply.data = new QueryOrderReplyOrder();
            if (apiQueryOrderReply.Status == "Success")
            {
                var screenInfo = _screenInfoService.GetScreenInfo(CinemaCode, order.orderBaseInfo.ScreenCode);
                var sessionInfo = _sessionInfoService.GetSessionInfo(CinemaCode,
                    order.orderBaseInfo.SessionCode);
                queryOrderReply.data.MapFrom(order, cinema, screenInfo, sessionInfo);
                queryOrderReply.SetSuccessReply();
            }
            else
            {
                queryOrderReply.GetErrorFromNetSaleReply(apiQueryOrderReply);
            }
            return queryOrderReply;

        }
        #endregion

        #region 查询影票信息
        [HttpGet]
        public QueryTicketReply QueryTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            QueryTicketReply queryTicketReply = new QueryTicketReply();
            //校验参数
            if (!queryTicketReply.RequestInfoGuard(UserName, Password, CinemaCode, PrintNo, VerifyCode))
            {
                return queryTicketReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryTicketReply.SetUserCredentialInvalidReply();
                return queryTicketReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryTicketReply.SetCinemaInvalidReply();
                return queryTicketReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithPrintNo(CinemaCode, PrintNo, VerifyCode);
            if (order == null)
            {
                queryTicketReply.SetOrderNotExistReply();
                return queryTicketReply;
            }
            NetSaleSvc.Api.Models.QueryTicketReply apiQueryTicketReply = _orderService.QueryTicket(order);
            queryTicketReply.data = new QueryTicketReplyTickets();
            if (apiQueryTicketReply.Status == "Success")
            {
                var screenInfo = _screenInfoService.GetScreenInfo(CinemaCode, order.orderBaseInfo.ScreenCode);
                var sessionInfo = _sessionInfoService.GetSessionInfo(CinemaCode,
                    order.orderBaseInfo.SessionCode);
                queryTicketReply.data.MapFrom(order, cinema, screenInfo);
                queryTicketReply.SetSuccessReply();
            }
            else
            {
                queryTicketReply.GetErrorFromNetSaleReply(apiQueryTicketReply);
            }
            return queryTicketReply;
        }
        #endregion
        #region 查询出票状态
        [HttpGet]
        public QueryPrintReply QueryPrint(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            QueryPrintReply queryPrintReply = new QueryPrintReply();
            //校验参数
            if (!queryPrintReply.RequestInfoGuard(UserName, Password, CinemaCode, PrintNo, VerifyCode))
            {
                return queryPrintReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryPrintReply.SetUserCredentialInvalidReply();
                return queryPrintReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryPrintReply.SetCinemaInvalidReply();
                return queryPrintReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithPrintNo(CinemaCode, PrintNo, VerifyCode);
            if (order == null)
            {
                queryPrintReply.SetOrderNotExistReply();
                return queryPrintReply;
            }
            NetSaleSvc.Api.Models.QueryPrintReply apiQueryPrintReply = _orderService.QueryPrint(order);
            queryPrintReply.data = new QueryPrintReplyOrder();
            if (apiQueryPrintReply.Status == "Success")
            {
                queryPrintReply.data.OrderCode = order.orderBaseInfo.SubmitOrderCode;
                queryPrintReply.data.PrintNo = order.orderBaseInfo.PrintNo;
                queryPrintReply.data.VerifyCode = order.orderBaseInfo.VerifyCode;
                queryPrintReply.data.Status = order.orderBaseInfo.PrintStatus == true ? YesOrNoEnum.Yes : YesOrNoEnum.No;
                queryPrintReply.data.PrintTime = order.orderBaseInfo.PrintTime?.ToFormatStringWithT() ?? string.Empty;
                queryPrintReply.SetSuccessReply();
            }
            else
            {
                queryPrintReply.GetErrorFromNetSaleReply(apiQueryPrintReply);
            }
            return queryPrintReply;

        }
        #endregion
        #region 确认出票接口
        [HttpGet]
        public FetchTicketReply FetchTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            FetchTicketReply fetchTicketReply = new FetchTicketReply();
            //校验参数
            if (!fetchTicketReply.RequestInfoGuard(UserName, Password, CinemaCode, PrintNo, VerifyCode))
            {
                return fetchTicketReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                fetchTicketReply.SetUserCredentialInvalidReply();
                return fetchTicketReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                fetchTicketReply.SetCinemaInvalidReply();
                return fetchTicketReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithPrintNo(CinemaCode, PrintNo, VerifyCode);
            if (order == null)
            {
                fetchTicketReply.SetOrderNotExistReply();
                return fetchTicketReply;
            }
            NetSaleSvc.Api.Models.FetchTicketReply apiFetchTicketReply = _orderService.FetchTicket(order);
            if(apiFetchTicketReply.Status== "Success")
            {
                fetchTicketReply.SetSuccessReply();
            }
            else
            {
                fetchTicketReply.GetErrorFromNetSaleReply(apiFetchTicketReply);
            }
            return fetchTicketReply;

        }
        #endregion
    }
}
