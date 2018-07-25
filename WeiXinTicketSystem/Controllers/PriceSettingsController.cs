using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.PriceSettings;
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
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem.Controllers
{
    public class PriceSettingsController : RootExraController
    {
        private CinemaPriceSettingService _priceSettingService;
        private CinemaService _cinemaService;
        #region ctor
        public PriceSettingsController()
        {
            _priceSettingService = new CinemaPriceSettingService();
            _cinemaService = new CinemaService();
        }
        #endregion
        /// <summary>
        /// 影院价格设置首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "PriceSettings").SingleOrDefault();
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
            var pricesettings = await _priceSettingService.GetPriceSettingsPagedAsync(
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(pricesettings.ToDynatableModel(
                pricesettings.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加价格设置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdatePriceSettingViewModel model = new CreateOrUpdatePriceSettingViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改价格设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var module = await _priceSettingService.GetAsync(id);
            if (module == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdatePriceSettingViewModel model = new CreateOrUpdatePriceSettingViewModel();
            model.MapFrom(module);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改价格设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdatePriceSettingViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改价格设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdatePriceSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaPriceSettingEntity pricesetting = new CinemaPriceSettingEntity();
            if (model.Id > 0)
            {
                pricesetting = await _priceSettingService.GetAsync(model.Id);
            }

            pricesetting.MapFrom(model);

            if (pricesetting.Id == 0)
            {
                await _priceSettingService.InsertAsync(pricesetting);
            }
            else
            {
                await _priceSettingService.UpdateAsync(pricesetting);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除价格设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var pricesetting = await _priceSettingService.GetAsync(id);

            if (pricesetting != null)
            {
                await _priceSettingService.DeleteAsync(pricesetting);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity>();
                cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
                ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.Name, Value = x.Code });
            }
            else
            {
                ViewBag.CinemaCode_dd = new List<SelectListItem>
                {
                    new SelectListItem { Text = CurrentUser.CinemaName, Value = CurrentUser.CinemaCode }
                };
            }
            ViewBag.IsSellByStandardPrice_dd = EnumUtil.GetSelectList<YesOrNoEnum>();
            ViewBag.FeePayType_dd = EnumUtil.GetSelectList<CinemaFeePayTypeEnum>();
            ViewBag.FeeGatherType_dd = EnumUtil.GetSelectList<CinemaFeeGatherTypeEnum>();
        }
    }
}