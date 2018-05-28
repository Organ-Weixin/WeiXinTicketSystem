﻿using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaTicketSystemAccount;
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
    public class TicketSystemAccountController : RootExraController
    {
        private CinemaTicketSystemAccountService _ticketSystemAccountService;

        #region ctor
        public TicketSystemAccountController()
        {
            _ticketSystemAccountService = new CinemaTicketSystemAccountService();
        }
        #endregion

        /// <summary>
        /// 影院系统对接账号配置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "TicketSystemAccount").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影院系统对接账号配置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaTicketSystemAccountQueryModel> pageModel)
        {
            var paySettings = await _ticketSystemAccountService.GetCinemaTicketSystemAccountPagedAsync(
                pageModel.Query.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影院系统对接账号配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateTicketSystemAccountViewModel model = new CreateOrUpdateTicketSystemAccountViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院系统对接账号配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var ticketSystemAccount = await _ticketSystemAccountService.GetCinemaTicketSystemAccountByIdAsync(id);
            if (ticketSystemAccount == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateTicketSystemAccountViewModel model = new CreateOrUpdateTicketSystemAccountViewModel();
            model.MapFrom(ticketSystemAccount);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院系统对接账号配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateTicketSystemAccountViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院系统对接账号配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateTicketSystemAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

          CinemaTicketSystemAccountEntity ticketSystemAccount = new CinemaTicketSystemAccountEntity();
            if (model.Id > 0)
            {
                ticketSystemAccount = await _ticketSystemAccountService.GetCinemaTicketSystemAccountByIdAsync(model.Id);
            }

            ticketSystemAccount.MapFrom(model);

            if (ticketSystemAccount.Id == 0)
            {
                await _ticketSystemAccountService.InsertAsync(ticketSystemAccount);
            }
            else
            {
                await _ticketSystemAccountService.UpdateAsync(ticketSystemAccount);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院系统对接账号配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _ticketSystemAccountService.GetCinemaTicketSystemAccountByIdAsync(id);

            if (cinema != null)
            {
                cinema.IsDel = true;
                await _ticketSystemAccountService.UpdateAsync(cinema);
            }
            return Object();
        }

        private void PreparyCreateOrEditViewData()
        {
            //绑定影院系统枚举
            ViewBag.TicketSystem_dd = EnumUtil.GetSelectList<CinemaTypeEnum>();

        }

    }
}