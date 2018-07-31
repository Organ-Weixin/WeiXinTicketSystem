using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.MiniProgramLinkUrl;
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
    public class MiniProgramLinkUrlController : RootExraController
    {
        private MiniProgramLinkUrlService _miniProgramLinkUrlService;

        #region ctor
        public MiniProgramLinkUrlController()
        {
            _miniProgramLinkUrlService = new MiniProgramLinkUrlService();
        }
        #endregion

        /// <summary>
        /// 链接地址管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "MiniProgramLinkUrl").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 链接地址列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<MiniProgramLinkUrlQueryModel> pageModel)
        {
            var miniProgramLinkUrl = await _miniProgramLinkUrlService.GetMiniProgramLinkUrlPagedAsync(
                pageModel.Query.LinkName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(miniProgramLinkUrl.ToDynatableModel(miniProgramLinkUrl.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加链接地址
        /// </summary>
        /// <returns></returns>
        public  ActionResult Create()
        {
            CreateOrUpdateMiniProgramLinkUrlViewModel model = new CreateOrUpdateMiniProgramLinkUrlViewModel();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改链接地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var miniProgramLinkUrl = await _miniProgramLinkUrlService.GetMiniProgramLinkUrlByIdAsync(id);
            if (miniProgramLinkUrl == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateMiniProgramLinkUrlViewModel model = new CreateOrUpdateMiniProgramLinkUrlViewModel();
            model.MapFrom(miniProgramLinkUrl);
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 添加或修改链接地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateMiniProgramLinkUrlViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改链接地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateMiniProgramLinkUrlViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            MiniProgramLinkUrlEntity miniProgramLinkUrl= new MiniProgramLinkUrlEntity();
            if (model.Id > 0)
            {
                miniProgramLinkUrl = await _miniProgramLinkUrlService.GetMiniProgramLinkUrlByIdAsync(model.Id);
            }

            miniProgramLinkUrl.MapFrom(model);

            if (miniProgramLinkUrl.Id == 0)
            {
                await _miniProgramLinkUrlService.InsertAsync(miniProgramLinkUrl);
            }
            else
            {
                await _miniProgramLinkUrlService.UpdateAsync(miniProgramLinkUrl);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除链接地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var miniProgramLinkUrl = await _miniProgramLinkUrlService.GetMiniProgramLinkUrlByIdAsync(id);

            if (miniProgramLinkUrl != null)
            {
                miniProgramLinkUrl.IsDel = true;
                await _miniProgramLinkUrlService.UpdateAsync(miniProgramLinkUrl);
            }
            return Object();
        }

    }
}