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
        #endregion

        [HttpPost]
        public LockSeatReply LockSeat(NetSaleQueryJson QueryJson)
        {
            LockSeatReply lockSeatReply = netSaleService.LockSeat(QueryJson.UserName, QueryJson.Password,QueryJson.QueryXml);
            //锁座时新增订单需要传入OpenID,之后修改订单就不需要再操作
            if(lockSeatReply.Status== "Success")
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

        [HttpGet]
        public RefundTicketReply RefundTicket(string UserName, string Password, string CinemaCode,string PrintNo, string VerifyCode)
        {
            return netSaleService.RefundTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public QueryOrderReply QueryOrder(string UserName, string Password, string CinemaCode,string OrderCode)
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
    }
}
