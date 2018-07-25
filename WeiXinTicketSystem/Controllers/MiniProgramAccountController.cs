using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaMiniProgramAccount;
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
    public class MiniProgramAccountController : RootExraController
    {
        private CinemaMiniProgramAccountService _miniProgramAccountService;
        private CinemaService _cinemaService;
        #region ctor
        public MiniProgramAccountController()
        {
            _miniProgramAccountService = new CinemaMiniProgramAccountService();
            _cinemaService = new CinemaService();
        }
        #endregion

        /// <summary>
        /// 影院系统对接账号配置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "MiniProgramAccount").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影院系统对接账号配置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaMiniProgramAccountQueryModel> pageModel)
        {
            var MiniPrograms = await _miniProgramAccountService.GetCinemaMiniProgramAccountPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(MiniPrograms.ToDynatableModel(MiniPrograms.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影院系统对接账号配置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateMiniProgramAccountViewModel model = new CreateOrUpdateMiniProgramAccountViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院系统对接账号配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var miniProgramAccount = await _miniProgramAccountService.GetCinemaMiniProgramAccountByIdAsync(id);
            if (miniProgramAccount == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateMiniProgramAccountViewModel model = new CreateOrUpdateMiniProgramAccountViewModel();
            model.MapFrom(miniProgramAccount);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院系统对接账号配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateMiniProgramAccountViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院系统对接账号配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateMiniProgramAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaMiniProgramAccountEntity miniProgramAccount = new CinemaMiniProgramAccountEntity();
            if (model.Id > 0)
            {
                miniProgramAccount = await _miniProgramAccountService.GetCinemaMiniProgramAccountByIdAsync(model.Id);
            }

            miniProgramAccount.MapFrom(model);

            if (miniProgramAccount.Id == 0)
            {
                await _miniProgramAccountService.InsertAsync(miniProgramAccount);
            }
            else
            {
                await _miniProgramAccountService.UpdateAsync(miniProgramAccount);
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
            var account = await _miniProgramAccountService.GetCinemaMiniProgramAccountByIdAsync(id);

            if (account != null)
            {
                account.IsDel = true;
                await _miniProgramAccountService.UpdateAsync(account);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定影院系统枚举
            ViewBag.TicketSystem_dd = EnumUtil.GetSelectList<CinemaTypeEnum>();
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