using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaPaySettings;
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
    public class PaySettingsController : RootExraController
    {
        private CinemaPaySettingsService _paySettingsService;
        private CinemaService _cinemaService;

        #region ctor
        public PaySettingsController()
        {
            _paySettingsService = new CinemaPaySettingsService();
            _cinemaService = new CinemaService();
        }
        #endregion


        /// <summary>
        /// 影院支付方式配置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "PaySettings").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影院支付方式配置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaPaySettingsQueryModel> pageModel)
        {
            var paySettings = await _paySettingsService.GetCinemaPaySettingsPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影院支付方式配置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateCinemaPaySettingsViewModel model = new CreateOrUpdateCinemaPaySettingsViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院支付方式配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var paySettings = await _paySettingsService.GetCinemaPaySettingsByIdAsync(id);
            if (paySettings == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaPaySettingsViewModel model = new CreateOrUpdateCinemaPaySettingsViewModel();
            model.MapFrom(paySettings);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaPaySettingsViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }


        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaPaySettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

           CinemaPaymentSettingEntity paySettings = new CinemaPaymentSettingEntity();
            if (model.Id > 0)
            {
                paySettings = await _paySettingsService.GetCinemaPaySettingsByIdAsync(model.Id);
            }

            paySettings.MapFrom(model);

            if (paySettings.Id == 0)
            {
                await _paySettingsService.InsertAsync(paySettings);
            }
            else
            {
                await _paySettingsService.UpdateAsync(paySettings);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院支付方式配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _paySettingsService.GetCinemaPaySettingsByIdAsync(id);

            if (cinema != null)
            {
                cinema.IsDel = true;
                await _paySettingsService.UpdateAsync(cinema);
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

            //绑定是否使用支付宝枚举
            ViewBag.IsUseAlipay_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定是否使用百度钱包枚举
            ViewBag.IsUseBfbpay_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定是否使用微信支付枚举
            ViewBag.IsUseWxpay_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定是否使用会员卡枚举
            ViewBag.IsUserMemberCard_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }
    }
}