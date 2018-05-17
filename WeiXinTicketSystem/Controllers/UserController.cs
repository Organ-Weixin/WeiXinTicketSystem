using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.User;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Controllers
{
    public class UserController : RootExraController
    {
        private SystemUserService _systemUserService;
        private CinemaService _cinemaService;
        private SystemRoleService _roleService;

        #region ctor
        public UserController()
        {
            _systemUserService = new SystemUserService();
            _cinemaService = new CinemaService();
            _roleService = new SystemRoleService();
        }
        #endregion

        /// <summary>
        /// 用户管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "User").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var users = await _systemUserService.GetUsersByUserIdAsync(CurrentUser.Id,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(users.ToDynatableModel(
                users.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateUserViewModel model = new CreateUserViewModel();

            await PreparyCreateOrEditViewData();

            return View();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            var existedUser = await _systemUserService.GetByUsernameAsync(model.Username);
            if (existedUser != null)
            {
                return ErrorObject("用户名已存在！");
            }

            CinemaEntity cinema;
            if (model.CinemaCode != Resources.DEFAULT_CINEMACODE)
            {
                cinema = await _cinemaService.GetCinemaByCinemaCodeAsync(model.CinemaCode);
                if (cinema == null)
                {
                    return ErrorObject("影院不存在");
                }
            }
            else
            {
                cinema = new CinemaEntity { CinemaCode = Resources.DEFAULT_CINEMACODE, CinemaName = "普照网络" };
            }

            var role = await _roleService.GetAsync(model.RoleId);
            if (role == null)
            {
                return ErrorObject("角色不存在");
            }

            SystemUserEntity sysUser = new SystemUserEntity();
            sysUser.MapFrom(model, cinema, role);
            sysUser.CreateUserId = CurrentUser.Id;
            sysUser.Created = DateTime.Now;
            await _systemUserService.InsertAsync(sysUser);

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var sysUser = await _systemUserService.GetAsync(id);
            if (sysUser == null)
            {
                return HttpBadRequest();
            }

            UpdateUserViewModel model = new UpdateUserViewModel();
            model.MapFrom(sysUser);

            await PreparyCreateOrEditViewData();

            return View(model);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            var sysUser = await _systemUserService.GetAsync(model.Id);
            if (sysUser == null)
            {
                return HttpBadRequest();
            }

            var existedUser = await _systemUserService.GetByUsernameAsync(model.Username);
            if (existedUser != null && existedUser.Id != sysUser.Id)
            {
                return ErrorObject("用户名已存在！");
            }

            sysUser.UserName = model.Username;
            sysUser.RealName = model.RealName;

            CinemaEntity cinema;
            if (model.CinemaCode != Resources.DEFAULT_CINEMACODE)
            {
                cinema = await _cinemaService.GetCinemaByCinemaCodeAsync(model.CinemaCode);
                if (cinema == null)
                {
                    return ErrorObject("影院不存在");
                }
            }
            else
            {
                cinema = new CinemaEntity { CinemaCode = Resources.DEFAULT_CINEMACODE, CinemaName = "普照网络" };
            }
            sysUser.CinemaCode = cinema.CinemaCode;
            sysUser.CinemaName = cinema.CinemaName;

            var role = await _roleService.GetAsync(model.RoleId);
            if (role == null)
            {
                return ErrorObject("角色不存在");
            }
            sysUser.RoleId = model.RoleId;
            sysUser.RoleName = role.Name;

            sysUser.UpdateUserId = CurrentUser.Id;
            sysUser.Updated = DateTime.Now;
            await _systemUserService.UpdateAsync(sysUser);

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var sysUser = await _systemUserService.GetAsync(id);

            if (sysUser != null && sysUser.CreateUserId != 0)
            {
                sysUser.Deleted = true;
                sysUser.Updated = DateTime.Now;
                await _systemUserService.UpdateAsync(sysUser);
            }

            return Object();
        }

        /// <summary>
        /// 用户管理修改密码
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ModifyPassword(int id)
        {
            var sysUser = await _systemUserService.GetAsync(id);
            if (sysUser == null)
            {
                return HttpBadRequest();
            }

            ModifyPasswordViewModel model = new ModifyPasswordViewModel();
            model.Id = sysUser.Id;

            ViewBag.Username = sysUser.UserName;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ModifyPassword(ModifyPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            var sysUser = await _systemUserService.GetAsync(model.Id);
            if (sysUser == null)
            {
                return ErrorObject("用户不存在");
            }

            await _systemUserService.UpdatePassword(sysUser, model.NewPassword);

            return Object();
        }

        #region 当前用户修改密码
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            var sysUser = await _systemUserService.LoginAsync(CurrentUser.UserName, model.OldPassword);
            if (sysUser == null)
            {
                return ErrorObject("修改密码失败：旧密码错误！");
            }

            await _systemUserService.UpdatePassword(sysUser, model.NewPassword);

            return Object();
        }
        #endregion

        /// <summary>
        /// 添加或修改用户时，权限选择下拉框数据
        /// </summary>
        /// <returns></returns>
        private async Task PreparyCreateOrEditViewData()
        {
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity> { new CinemaEntity { CinemaCode = Resources.DEFAULT_CINEMACODE, CinemaName = "普照网络" } };
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

            var roles = await _roleService.GetAllRolesAsync(CurrentUser.Id);
            ViewBag.RoleId_dd = roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        }
    }
}