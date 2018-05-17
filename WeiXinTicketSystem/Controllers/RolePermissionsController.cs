using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.RolePermission;
using WeiXinTicketSystem.Models.User;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Controllers
{
    public class RolePermissionsController : RootExraController
    {
        SystemRolePermissionService _rolePermissionService;
        SystemModuleService _moduleService;
        #region ctor
        public RolePermissionsController()
        {
            _rolePermissionService = new SystemRolePermissionService();
            _moduleService = new SystemModuleService();
        }
        #endregion
        public ActionResult Index(int RoleId)
        {
            ViewBag.RoleId = RoleId;
            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<RolePermissionQueryModel> pageModel)
        {
            var UserCinemas = await _rolePermissionService.GetRolePermissionsPagedAsync(
                pageModel.Query.RoleId,
                pageModel.Query.ModuleName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(UserCinemas.ToDynatableModel(UserCinemas.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增接入商影院
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create(int RoleId)
        {
            CreateOrUpdateRolePermissionViewModel model = new CreateOrUpdateRolePermissionViewModel();

            model.RoleId = RoleId;

            await PreparyCreateOrEditViewData();

            return View(nameof(CreateOrUpdate), model);
        }
        /// <summary>
        /// 编辑接入商影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var rolePermission = await _rolePermissionService.GetRolePermissionByIdAsync(id);
            if (rolePermission == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateRolePermissionViewModel model = new CreateOrUpdateRolePermissionViewModel();
            model.MapFrom(rolePermission);

            await PreparyCreateOrEditViewData();

            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 删除接入商影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var rolePermission = await _rolePermissionService.GetRolePermissionByIdAsync(id);
            if (rolePermission != null && rolePermission.Id > 0)
            {
                await _rolePermissionService.DeleteAsync(rolePermission);
            }
            return Object();
        }

        /// <summary>
        /// 添加或修改接入商影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateRolePermissionViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改接入商影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateRolePermissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            SystemRolePermissionEntity rolePermission = new SystemRolePermissionEntity();
            if (model.Id > 0)
            {
                rolePermission = await _rolePermissionService.GetRolePermissionByIdAsync(model.Id);
            }
            rolePermission.MapFrom(model);
            if (rolePermission.Id == 0)
            {
                //判断是否已经存在
                var existedrolepermission = await _rolePermissionService.GetRolePermissionByRoleIdAndModuleId(model.RoleId, int.Parse(model.ModuleId));
                if (existedrolepermission != null)
                {
                    return ErrorObject("模块权限已存在！");
                }
                await _rolePermissionService.InsertAsync(rolePermission);
            }
            else
            {
                await _rolePermissionService.UpdateAsync(rolePermission);
            }
            return RedirectObject(Url.Action(nameof(Index)) + "?queries[RoleId]=" + model.RoleId + "&RoleId=" + model.RoleId);
        }

        private async Task PreparyCreateOrEditViewData()
        {
            var allModules = await _moduleService.GetAllModulesAsync();
            ViewBag.ModuleId_dd = allModules.Select(x => new SelectListItem { Text = x.ModuleName, Value = x.Id.ToString() });

            ViewBag.Permissions_dd = EnumUtil.GetSelectList<SystemPermissionEnum>();
        }
    }
}