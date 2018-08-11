using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.UserInfo;
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
    public class UserInfoController : RootExraController
    {
        private UserInfoService _userinfoService;

        #region ctor
        public UserInfoController()
        {
            _userinfoService = new UserInfoService();
        }
        #endregion
        /// <summary>
        /// 接入商首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<UserInfoQueryModel> pageModel)
        {
            var UserInfos = await _userinfoService.GetUserInfosPagedAsync(
                pageModel.Query.UserName,
                pageModel.Query.Company,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(UserInfos.ToDynatableModel(UserInfos.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增接入商
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateUserInfoViewModel model = new CreateOrUpdateUserInfoViewModel();

            return View(nameof(CreateOrUpdate), model);
        }
        /// <summary>
        /// 编辑接入商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var userInfo = await _userinfoService.GetUserInfoByIdAsync(id);
            if (userInfo == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateUserInfoViewModel model = new CreateOrUpdateUserInfoViewModel();
            model.MapFrom(userInfo);
            
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 删除接入商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var userInfo = await _userinfoService.GetUserInfoByIdAsync(id);
            if (userInfo != null && userInfo.Id > 0)
            {
                userInfo.IsDel = true;
                await _userinfoService.UpdateAsync(userInfo);
            }
            return Object();
        }

        /// <summary>
        /// 添加或修改接入商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateUserInfoViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改接入商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateUserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            UserInfoEntity userInfo = new UserInfoEntity();
            if (model.Id > 0)
            {
                userInfo = await _userinfoService.GetUserInfoByIdAsync(model.Id);
            }
            userInfo.MapFrom(model);
            if (userInfo.Id == 0)
            {
                //判断是否已经存在
                var existedmiddleware = await _userinfoService.GettUserInfoByUserNameAsync(model.UserName);
                if (existedmiddleware != null)
                {
                    return ErrorObject("接入商已存在！");
                }
                userInfo.Advance = 0;
                userInfo.IsDel = false;
                await _userinfoService.InsertAsync(userInfo);
            }
            else
            {
                await _userinfoService.UpdateAsync(userInfo);
            }
            return RedirectObject(Url.Action(nameof(Index)));
        }
    }
}