using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Service;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace WeiXinTicketSystem.Controllers
{
    [RoutePrefix("")]
    public class LoginController : RootBaseController
    {
        private SystemUserService _systemUserService;
        private CinemaService _cinemaService;

        #region ctor
        public LoginController()
        {
            _systemUserService = new SystemUserService();
            _cinemaService = new CinemaService();
        }
        #endregion

        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        [HttpGet]
        public ActionResult Index()
        {
            SysUserLoginViewModel model = new SysUserLoginViewModel();
            return View(model);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Index(SysUserLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var sysUser = await _systemUserService.LoginAsync(model.Username, model.Password);
            if (sysUser == null)
            {
                ModelState.AddModelError("", "用户名或密码不正确");
                return View(model);
            }

            //写入缓存
            FormsAuthentication.SetAuthCookie(sysUser.Id.ToString(), false);

            return RedirectToLocal(returnUrl);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Route("LogOut")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(MvcNames.Login.Index);
        }

        /// <summary>
        /// 跳转本地路径(防止CSRF攻击)
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(MvcNames.Home.Index, MvcNames.Home.Name);
        }
    }
}