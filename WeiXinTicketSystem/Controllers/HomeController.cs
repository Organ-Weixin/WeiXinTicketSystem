using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Drawing;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Controllers
{
    public class HomeController : RootExraController
    {
        //private HomeService _homeService;
        #region ctor
        public HomeController()
        {
            //_homeService = new HomeService();
        }
        #endregion
        /// <summary>
        /// 登陆后的首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}