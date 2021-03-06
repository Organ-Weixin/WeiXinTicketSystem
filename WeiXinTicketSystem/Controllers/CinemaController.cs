﻿using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Cinema;
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
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem.Controllers
{
    public class CinemaController : RootExraController
    {
        private CinemaService _cinemaService;
        private MiddlewareService _middlewareService;

        #region ctor
        public CinemaController()
        {
            _cinemaService = new CinemaService();
            _middlewareService = new MiddlewareService();
        }
        #endregion


        /// <summary>
        /// 影院管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Cinema").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            PrepareIndexViewData();
            return View();
        }

        ///// <summary>
        ///// 影院列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaQueryModel> pageModel)
        {
            var Cinemas = await _cinemaService.GetCinemasPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.IsOpen,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(Cinemas.ToDynatableModel(Cinemas.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影院
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateCinemaViewModel model = new CreateOrUpdateCinemaViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinema == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaViewModel model = new CreateOrUpdateCinemaViewModel();
            model.MapFrom(cinema);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaEntity cinema = new CinemaEntity();
            if (model.Id > 0)
            {
                cinema = await _cinemaService.GetCinemaByIdAsync(model.Id);
            }

            cinema.MapFrom(model);

            if (cinema.Id == 0)
            {
                cinema.Created = DateTime.Now;
                await _cinemaService.InsertAsync(cinema);
            }
            else
            {
                cinema.Updated = DateTime.Now;
                await _cinemaService.UpdateAsync(cinema);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);

            if (cinema != null)
            {
                cinema.IsDel = true;
                cinema.Updated = DateTime.Now;
                await _cinemaService.UpdateAsync(cinema);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定中间件枚举
            List<MiddlewareEntity> middlewares = new List<MiddlewareEntity>();
            middlewares.AddRange(await _middlewareService.GetAllMiddlewaresAsync());
            ViewBag.Mid_dd = middlewares.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            //绑定所属院线枚举
            ViewBag.TheaterChain_dd = EnumUtil.GetSelectList<TheaterChainEnum>();

            //绑定状态(1-开通，2-关闭)枚举
            ViewBag.IsOpen_dd = EnumUtil.GetSelectList<CinemaOpenEnum>();

            //绑定是否开通套餐枚举
            ViewBag.OpenSnacks_dd = EnumUtil.GetSelectList<CinemaOpenEnum>();

            //绑定是否小卖配送枚举
            ViewBag.IsSnackDistribution_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }

        private void PrepareIndexViewData()
        {
            ViewBag.IsOpen = EnumUtil.GetSelectList<CinemaOpenEnum>();
        }


    }
}