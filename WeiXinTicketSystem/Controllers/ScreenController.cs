using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Screen;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.IO;
using WeiXinTicketSystem.Entity.Models;

namespace WeiXinTicketSystem.Controllers
{
    public class ScreenController : RootExraController
    {
        private ScreenInfoService _screenInfoService;
        private SeatInfoService _seatInfoService;
        private CinemaService _cinemaService;
        #region ctor
        public ScreenController()
        {
            _screenInfoService = new ScreenInfoService();
            _seatInfoService = new SeatInfoService();
            _cinemaService = new CinemaService();
        }
        #endregion
        /// <summary>
        /// 影厅管理首页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            await PrepareIndexViewData();
            return View();
        }
        public async Task<ActionResult> List(DynatablePageModel<ScreenInfoQueryModel> pageModel)
        {
            var Cinemas = await _screenInfoService.GetScreenInfoPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode_dd : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(Cinemas.ToDynatableModel(Cinemas.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        public async Task<ActionResult> Update()
        {
            UpdateScreenViewModel model = new UpdateScreenViewModel();
            await PrepareIndexViewData();
            return View(model);
        }

        [HttpPost]
        public ActionResult _Update(UpdateScreenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            ReturnData returnData = _screenInfoService.QueryCinema(CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? model.CinemaCode : CurrentUser.CinemaCode);
            if (!returnData.Status)
            {
                return ErrorObject(returnData.Info);
            }
            return RedirectObject(Url.Action(nameof(Index)) + "?queries[CinemaCode_dd]=" + model.CinemaCode);
            //return Object();
            //return View(nameof(Index));
        }

        public async Task<ActionResult> UdateScreenSeat(int id)
        {
            var screenInfo = await _screenInfoService.GetScreenInfoByIdAsync(id);
            if (screenInfo != null)
            {
                var returnData = _seatInfoService.QuerySeat(screenInfo.CinemaCode, screenInfo.ScreenCode);
                if (!returnData.Status)
                {
                    return ErrorObject(returnData.Info);
                }
            }
            return Object();
        }

        /// <summary>
        /// 准备首页下拉框数据
        /// </summary>
        /// <returns></returns>
        private async Task PrepareIndexViewData()
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
    }
}