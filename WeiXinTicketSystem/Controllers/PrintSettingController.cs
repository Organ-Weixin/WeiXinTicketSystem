using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.CinemaPrintSetting;
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
    public class PrintSettingController : RootExraController
    {
        private CinemaPrintSettingService _printSettingService;

        #region ctor
        public PrintSettingController()
        {
            _printSettingService = new CinemaPrintSettingService();
        }
        #endregion


        /// <summary>
        /// 影院打印设置管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "PrintSetting").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 影院打印设置列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<CinemaPrintSettingQueryModel> pageModel)
        {
            var printSetting = await _printSettingService.GetCinemaPrintSettingPagedAsync(
                pageModel.Query.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(printSetting.ToDynatableModel(printSetting.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加影院打印设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateCinemaPrintSettingViewModel model = new CreateOrUpdateCinemaPrintSettingViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改影院打印设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var printSetting = await _printSettingService.GetCinemaPrintSettingByIdAsync(id);
            if (printSetting == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaPrintSettingViewModel model = new CreateOrUpdateCinemaPrintSettingViewModel();
            model.MapFrom(printSetting);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院打印设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaPrintSettingViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院打印设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaPrintSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaPrintSettingEntity printSetting = new CinemaPrintSettingEntity();
            if (model.Id > 0)
            {
                printSetting = await _printSettingService.GetCinemaPrintSettingByIdAsync(model.Id);
            }

            printSetting.MapFrom(model);

            if (printSetting.Id == 0)
            {
                await _printSettingService.InsertAsync(printSetting);
            }
            else
            {
                await _printSettingService.UpdateAsync(printSetting);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院打印设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var printSetting = await _printSettingService.GetCinemaPrintSettingByIdAsync(id);

            if (printSetting != null)
            {
                printSetting.IsDel = true;
                await _printSettingService.UpdateAsync(printSetting);
            }
            return Object();
        }

        private void PreparyCreateOrEditViewData()
        {
            //绑定取票是否打印各数据项名称枚举
            ViewBag.IsPrintName_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定影院是否已客制化化过票版枚举
            ViewBag.IsCustomTicketTemplet_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

            //绑定影院是否已客制化过套餐模板枚举
            ViewBag.IsCustomPackageTemplet_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }
    }
}