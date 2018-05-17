using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Module;
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

namespace WeiXinTicketSystem.Controllers
{
    public class ModuleController : RootExraController
    {
        public SystemModuleService _moduleService;
        #region ctor
        public ModuleController()
        {
            _moduleService = new SystemModuleService();
        }
        #endregion

        /// <summary>
        /// 模块管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Module").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 模块列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var modules = await _moduleService.GetModulesPagedAsync(CurrentUser.Id,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(modules.ToDynatableModel(
                modules.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateModuleViewModel model = new CreateOrUpdateModuleViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var module = await _moduleService.GetAsync(id);
            if (module == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateModuleViewModel model = new CreateOrUpdateModuleViewModel();
            model.MapFrom(module);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateModuleViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateModuleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            SystemModuleEntity module = new SystemModuleEntity();
            if (model.Id > 0)
            {
                module = await _moduleService.GetAsync(model.Id);
            }

            module.MapFrom(model);

            if (module.Id == 0)
            {
                module.CreateUserId = CurrentUser.Id;
                module.Created = DateTime.Now;
                module.Deleted = false;
                await _moduleService.InsertAsync(module);
            }
            else
            {
                module.Updated = DateTime.Now;
                await _moduleService.UpdateAsync(module);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var module = await _moduleService.GetAsync(id);

            if (module != null)
            {
                module.Deleted = true;
                module.Updated = DateTime.Now;
                await _moduleService.UpdateAsync(module);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            var rootModules = await _moduleService.GetRootModulesAsync();

            List<SelectListItem> rootModulesList = new List<SelectListItem>();
            
            rootModulesList = rootModules.Select(x => new SelectListItem { Text = x.ModuleName, Value = x.Id.ToString() }).ToList();
            rootModulesList.Add(new SelectListItem { Text = "根模块", Value = "0" });

            ViewBag.ModuleParentId_dd = rootModulesList;
        }
    }
}