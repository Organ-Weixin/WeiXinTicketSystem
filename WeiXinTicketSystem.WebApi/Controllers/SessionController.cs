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
    public class SessionController : ApiController
    {
        #region 私有变量
        /// <summary>
        /// 服务引用
        /// </summary>
        private NetSaleSvcCore netSaleService = NetSaleSvcCore.Instance;
        #endregion

        [HttpGet]
        public QuerySessionReply QuerySessions(string UserName, string Password, string CinemaCode,string StartDate,string EndDate)
        {
            return netSaleService.QuerySession(UserName, Password, CinemaCode, StartDate, EndDate);
        }

        [HttpGet]
        public QuerySessionSeatReply QuerySessionSeat(string UserName, string Password, string CinemaCode, string SessionCode, string Status)
        {
            return netSaleService.QuerySessionSeat(UserName,Password,CinemaCode,SessionCode,Status);
        }
    }
}