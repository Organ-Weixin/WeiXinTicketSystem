using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Session;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Service;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models;

namespace WeiXinTicketSystem.Controllers
{
    public class SessionController : RootExraController
    {
        private SessionInfoService _sessionInfoService;
        private SessionPriceSettingService _priceSettingService;
        private CinemaService _cinemaService;

        #region ctor
        public SessionController()
        {
            _sessionInfoService = new SessionInfoService();
            _priceSettingService = new SessionPriceSettingService();
            _cinemaService = new CinemaService();
        }
        #endregion

        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Session").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<SessionPageQueryModel> pageModel)
        {
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(pageModel.Query.SessionDateRange))
            {
                var dates = pageModel.Query.SessionDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }
            var sessions = await _sessionInfoService.GetSessionsPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search,
                 startDate,
                 endDate);

            return DynatableResult(sessions.ToDynatableModel(
                sessions.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        public async Task<ActionResult> Update()
        {
            UpdateSessionViewModel model = new UpdateSessionViewModel();
            await PrepareUpdateViewData();
            return View(model);
        }

        [HttpPost]
        public ActionResult _Update(UpdateSessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            DateTime startDate, endDate;
            if (!string.IsNullOrEmpty(model.SessionDateRange))
            {
                var dates = model.SessionDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }
            else
            {
                startDate = DateTime.Now;
                endDate = DateTime.Now;
            }

            ReturnData returnData = _sessionInfoService.QuerySession(CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? model.CinemaCode : CurrentUser.CinemaCode,startDate,endDate);
            if (!returnData.Status)
            {
                return ErrorObject(returnData.Info);
            }
            return RedirectObject(Url.Action(nameof(Index)));
            //return Object();
            //return View(nameof(Index));
        }

        /// <summary>
        /// 价格设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> SetPrice(int id)
        {
            var adminsessionview = await _sessionInfoService.GetAsync(id);
            if (adminsessionview == null)
            {
                return HttpBadRequest();
            }

            SetPriceViewModel model = new SetPriceViewModel();

            model.MapFrom(adminsessionview);

            PreparySetPriceViewData();

            return PricePlan(model);
        }

        /// <summary>
        /// 价格计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PricePlan(SetPriceViewModel model)
        {
            return View(nameof(PricePlan), model);
        }

        /// <summary>
        /// 价格设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _SetPrice(SetPriceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            SessionPriceSettingEntity pricesetting = new SessionPriceSettingEntity();
            //if (model.PricePlanId > 0)
            //{
            //priceplan = await _pricePlanService.GetAsync(model.PricePlanId);
            //}

            pricesetting.MapFrom(model);
            SessionPriceSettingEntity Oldpriceplan = await _priceSettingService.GetAsync(pricesetting.CinemaCode, pricesetting.Type, pricesetting.Code);
            if (Oldpriceplan == null)
            {
                pricesetting.Id = 0;
            }
            else
            {
                pricesetting.Id = Oldpriceplan.Id;
            }
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
        /// 准备更新排期下拉框数据
        /// </summary>
        /// <returns></returns>
        private async Task PrepareUpdateViewData()
        {
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity>();
                cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
                ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.CinemaName, Value = x.CinemaCode });
            }
            else
            {
                ViewBag.CinemaCode_dd = new List<SelectListItem>
                {
                    new SelectListItem { Text = CurrentUser.CinemaName, Value = CurrentUser.CinemaCode }
                };
            }
        }

        /// <summary>
        /// 价格设置时，类型选择下拉框数据
        /// </summary>
        /// <returns></returns>
        private void PreparySetPriceViewData()
        {
            IEnumerable<SelectListItem> typeList = EnumUtil.GetSelectList<PricePlanTypeEnum>();
            ViewBag.Type_dd = typeList;
        }
    }
}