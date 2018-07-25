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

using NetSaleSvc.Api.Models;
using NetSaleSvc.Api.Core;

namespace WeiXinTicketSystem.Controllers
{
    public class ScreenController : RootExraController
    {
        private ScreenInfoService _screenInfoService;
        private SeatInfoService _seatInfoService;
        private CinemaService _cinemaService;
        private NetSaleSvcCore netSaleService;
        #region ctor
        public ScreenController()
        {
            _screenInfoService = new ScreenInfoService();
            _seatInfoService = new SeatInfoService();
            _cinemaService = new CinemaService();
            netSaleService = NetSaleSvcCore.Instance;
        }
        #endregion
        /// <summary>
        /// 影厅管理首页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Screen").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
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

            var querycinemaReply = netSaleService.QueryCinema("MiniProgram", "6BF477EBCC446F54E6512AFC0E976C41", CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? model.CinemaCode : CurrentUser.CinemaCode);
            if (querycinemaReply.Status != "Success")
            {
                return ErrorObject(querycinemaReply.ErrorMessage);
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
                var querySeatReply = netSaleService.QuerySeat("MiniProgram", "6BF477EBCC446F54E6512AFC0E976C41", screenInfo.CCode, screenInfo.SCode);
                if (querySeatReply.Status!= "Success")
                {
                    return ErrorObject(querySeatReply.ErrorMessage);
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