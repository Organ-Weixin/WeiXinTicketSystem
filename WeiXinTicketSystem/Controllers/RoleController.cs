using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Role;
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
    public class RoleController : RootExraController
    {
        private SystemRoleService _roleService;

        #region ctor
        public RoleController()
        {
            _roleService = new SystemRoleService();
        }
        #endregion

        /// <summary>
        /// 角色管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
             var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Role").SingleOrDefault();
             List<int> CurrentPermissions=menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var roles = await _roleService.GetRolesByUserIdAsync(CurrentUser.Id,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(roles.ToDynatableModel(
                roles.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateRoleViewModel model = new CreateOrUpdateRoleViewModel();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var role = await _roleService.GetAsync(id);
            if (role == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateRoleViewModel model = new CreateOrUpdateRoleViewModel();
            model.MapFrom(role);
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateRoleViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            SystemRoleEntity role = new SystemRoleEntity();
            if (model.Id > 0)
            {
                role = await _roleService.GetAsync(model.Id);
            }

            role.MapFrom(model);

            if (role.Id == 0)
            {
                role.Type = SystemRoleTypeEnum.Customized;
                role.CreateUserId = CurrentUser.Id;
                role.Created = DateTime.Now;
                await _roleService.InsertAsync(role);
            }
            else
            {
                role.Updated = DateTime.Now;
                await _roleService.UpdateAsync(role);
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
            var role = await _roleService.GetAsync(id);

            if (role != null && role.Type == SystemRoleTypeEnum.Customized)
            {
                role.Deleted = true;
                role.Updated = DateTime.Now;
                await _roleService.UpdateAsync(role);
            }
            return Object();
        }
    }
}