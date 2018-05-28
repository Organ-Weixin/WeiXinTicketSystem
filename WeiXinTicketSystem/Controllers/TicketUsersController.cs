using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.TicketUsers;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Service;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;
using System.Web;

namespace WeiXinTicketSystem.Controllers
{
    public class TicketUsersController : RootExraController
    {
        private TicketUsersService _ticketUsersService;

        #region ctor
        public TicketUsersController()
        {
            _ticketUsersService = new TicketUsersService();
        }
        #endregion


        /// <summary>
        /// 购票用户管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "TicketUsers").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 购票用户列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<TicketUsersQueryModel> pageModel)
        {
            var ticketUsers = await _ticketUsersService.GetTicketUserPagedAsync(
                pageModel.Query.MobilePhone,
                pageModel.Query.NickName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(ticketUsers.ToDynatableModel(ticketUsers.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }
    }
}