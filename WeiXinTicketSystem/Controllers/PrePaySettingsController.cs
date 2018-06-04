using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaPrePay;
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
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem.Controllers
{
    public class PrePaySettingsController : RootExraController
    {
        private CinemaPrePaySettingsService _prePayService;

        #region ctor
        public PrePaySettingsController()
        {
            _prePayService = new CinemaPrePaySettingsService();
        }
        #endregion

        /// <summary>
        /// 影院预付款配置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "PrePaySettings").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影院预付款配置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaPrePayQueryModel> pageModel)
        {
            var paySettings = await _prePayService.GetCinemaPrePaySettingsPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加影院预付款配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateCinemaPrePayViewModel model = new CreateOrUpdateCinemaPrePayViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院预付款配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var prePay = await _prePayService.GetCinemaPrePaySettingsByIdAsync(id);
            if (prePay == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaPrePayViewModel model = new CreateOrUpdateCinemaPrePayViewModel();
            model.MapFrom(prePay);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院预付款配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaPrePayViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院预付款配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaPrePayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

           CinemaPrePaySettingEntity prePay = new CinemaPrePaySettingEntity();
            if (model.Id > 0)
            {
                prePay = await _prePayService.GetCinemaPrePaySettingsByIdAsync(model.Id);
            }

            prePay.MapFrom(model);

            if (prePay.Id == 0)
            {
                await _prePayService.InsertAsync(prePay);
            }
            else
            {
                await _prePayService.UpdateAsync(prePay);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院预付款配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var prePay = await _prePayService.GetCinemaPrePaySettingsByIdAsync(id);

            if (prePay != null)
            {
                prePay.IsDel = true;
                await _prePayService.UpdateAsync(prePay);
            }
            return Object();
        }

        private void PreparyCreateOrEditViewData()
        {
            //绑定是否支付预付款枚举
            ViewBag.IsPrePay_dd = EnumUtil.GetSelectList<YesOrNoEnum>();


        }

    }
}