using System.Web.Mvc;
using WeiXinTicketSystem.Attributes.Authorize;
using WeiXinTicketSystem.Entity.Models;
using System.Collections.Generic;
using System.Linq;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.Service;
using System;

namespace WeiXinTicketSystem.Controllers
{
    /// <summary>
    /// 后台中所有页面控制器的基类
    /// </summary>
    [AdminAuthorize]
    public class RootExraController : RootBaseController
    {
        private SystemRoleService _systemRoleService;
        public RootExraController()
        {
            _systemRoleService = new SystemRoleService();
        }
        #region protected Property
        /// <summary>
        /// 当前用户
        /// </summary>
        protected SystemUserEntity CurrentUser
        {
            get
            {
                return ViewBag.SysUser;
            }
        }

        /// <summary>
        /// 当前用户所属角色
        /// </summary>
        protected SystemRoleEntity CurrentRole
        {
            get
            {
                return ViewBag.Role;
            }
        }

        /// <summary>
        /// 当前用户拥有的菜单
        /// </summary>
        protected List<SystemMenuViewEntity> CurrentSystemMenu
        {
            get
            {
                try
                {
                    return _systemRoleService.GetSystemMenuByRoleId(CurrentRole.Id).ToList();
                }
                catch(Exception ex)
                {
                    return new List<SystemMenuViewEntity>();
                }
            }
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.SysUserName = string.IsNullOrEmpty(CurrentUser.RealName) ? CurrentUser.UserName : CurrentUser.RealName;
            ViewBag.CurrentSystemMenu = CurrentSystemMenu;
            ViewBag.IsAdmin = CurrentRole == null ? false : CurrentRole.Type == SystemRoleTypeEnum.SystemAdmin;

            base.OnActionExecuting(filterContext);
        }
    }
}