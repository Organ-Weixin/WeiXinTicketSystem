using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.PricePlan;
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

namespace WeiXinTicketSystem.Controllers
{
    public class PricePlanController : RootExraController
    {
        public PricePlanService _pricePlanService;
        #region ctor
        public PricePlanController()
        {
            _pricePlanService = new PricePlanService();
        }
        #endregion
        /// <summary>
        /// 价格设置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "PricePlan").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 价格设置列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var pricesettings = await _pricePlanService.GetPricePlanPagedAsync(
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(pricesettings.ToDynatableModel(
                pricesettings.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 删除价格设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var pricesetting = await _pricePlanService.GetAsync(id);

            if (pricesetting != null)
            {
                await _pricePlanService.DeleteAsync(pricesetting);
            }
            return Object();
        }
    }
}