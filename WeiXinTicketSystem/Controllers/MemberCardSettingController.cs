using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaMemberCardSetting;
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
    public class MemberCardSettingController : RootExraController
    {
        private CinemaMemberCardSettingService _memberCardSettingService;
        private CinemaService _cinemaService;
        #region ctor
        public MemberCardSettingController()
        {
            _memberCardSettingService = new CinemaMemberCardSettingService();
            _cinemaService = new CinemaService();
        }
        #endregion

        /// <summary>
        /// 影城会员卡设置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "MemberCardSetting").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影城会员卡设置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaMemberCardSettingQueryModel> pageModel)
        {
            var paySettings = await _memberCardSettingService.GetCinemaMemberCardSettingPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影城会员卡设置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateCinemaMemberCardSettingViewModel model = new CreateOrUpdateCinemaMemberCardSettingViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影城会员卡设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var memberCardSetting = await _memberCardSettingService.GetCinemaMemberCardSettingByIdAsync(id);
            if (memberCardSetting == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaMemberCardSettingViewModel model = new CreateOrUpdateCinemaMemberCardSettingViewModel();
            model.MapFrom(memberCardSetting);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影城会员卡设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaMemberCardSettingViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影城会员卡设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaMemberCardSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaMemberCardSettingEntity memberCardSetting = new CinemaMemberCardSettingEntity();
            if (model.Id > 0)
            {
                memberCardSetting = await _memberCardSettingService.GetCinemaMemberCardSettingByIdAsync(model.Id);
            }

            memberCardSetting.MapFrom(model);

            if (memberCardSetting.Id == 0)
            {
                await _memberCardSettingService.InsertAsync(memberCardSetting);
            }
            else
            {
                await _memberCardSettingService.UpdateAsync(memberCardSetting);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影城会员卡设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _memberCardSettingService.GetCinemaMemberCardSettingByIdAsync(id);

            if (cinema != null)
            {
                cinema.IsDel = true;
                await _memberCardSettingService.UpdateAsync(cinema);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定是否使用会员卡枚举
            ViewBag.IsCardUse_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定是否使用会员卡注册枚举
            ViewBag.IsCardRegister_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定是否使用会员卡充值枚举
            ViewBag.IsCardReCharge_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定会员卡类型枚举
            ViewBag.CardType_dd = EnumUtil.GetSelectList<MemberCardTypeEnum>();

            //影院下拉
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
        }

    }
}