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
    public class OrderController : ApiController
    {
        OrderService _orderService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        TicketUsersService _ticketUserService;

        #region ctor
        public OrderController()
        {
            _orderService = new OrderService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _ticketUserService = new TicketUsersService();
        }
        #endregion

        #region 用户购票订单列表

        [HttpGet]
        public async Task<QueryOrdersReply> QueryOrders(string UserName, string Password, string CinemaCode, string OpenID, string startDate, string endDate, string CurrentPage, string PageSize)
        {
            QueryOrdersReply querOrdersReply = new QueryOrdersReply();
            //校验参数
            if (!querOrdersReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID, startDate, endDate, CurrentPage, PageSize))
            {
                return querOrdersReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                querOrdersReply.SetUserCredentialInvalidReply();
                return querOrdersReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                querOrdersReply.SetCinemaInvalidReply();
                return querOrdersReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                querOrdersReply.SetOpenIDNotExistReply();
                return querOrdersReply;
            }
            
            var Orders = await _orderService.GetOrdersByOpenIDPagedAsync(CinemaCode, OpenID, DateTime.Parse(startDate),DateTime.Parse(endDate), int.Parse(CurrentPage), int.Parse(PageSize));

            querOrdersReply.data = new QueryOrdersReplyOrders();
            if (Orders == null || Orders.Count == 0)
            {
                querOrdersReply.data.OrderCount = 0;
            }
            else
            {
                querOrdersReply.data.OrderCount= Orders.Count;
                querOrdersReply.data.Orders = Orders.Select(x => new QueryOrdersReplyOrder().MapFrom(x)).ToList();
            }
            querOrdersReply.SetSuccessReply();
            return querOrdersReply;
        }

        #endregion
    }
}