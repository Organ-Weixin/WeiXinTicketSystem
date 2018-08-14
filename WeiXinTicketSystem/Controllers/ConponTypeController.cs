using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.ConponType;
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
    public class ConponTypeController : RootExraController
    {
        public ConponTypeService _conponTypeService;
        #region ctor
        public ConponTypeController()
        {
            _conponTypeService = new ConponTypeService();
        }
        #endregion

        /// <summary>
        /// 优惠券类型管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "ConponType").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 优惠券类型列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var conponType = await _conponTypeService.GetConponTypePagedAsync(
                  pageModel.Query.Search,
                  pageModel.Offset,
                  pageModel.PerPage);

            return DynatableResult(conponType.ToDynatableModel(
                conponType.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加优惠券类型
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateConponTypeViewModel model = new CreateOrUpdateConponTypeViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改优惠券类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var conponType = await _conponTypeService.GetConponTypeByIdAsync(id);
            if (conponType == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateConponTypeViewModel model = new CreateOrUpdateConponTypeViewModel();
            model.MapFrom(conponType);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改优惠券类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateConponTypeViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改优惠券类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateConponTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            ConponTypeEntity conponType = new ConponTypeEntity();
            if (model.Id > 0)
            {
                conponType = await _conponTypeService.GetConponTypeByIdAsync(model.Id);
            }

            conponType.MapFrom(model);

            if (conponType.Id == 0)
            {
                conponType.Created = DateTime.Now;
                conponType.IsDel = false;
                await _conponTypeService.InsertAsync(conponType);
            }
            else
            {
                conponType.Updated = DateTime.Now;
                await _conponTypeService.UpdateAsync(conponType);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除优惠券类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var conponType = await _conponTypeService.GetConponTypeByIdAsync(id);

            if (conponType != null)
            {
                conponType.IsDel = true;
                conponType.Updated = DateTime.Now;
                await _conponTypeService.UpdateAsync(conponType);
            }
            return Object();
        }


        private async Task PreparyCreateOrEditViewData()
        {
            var rootConponType = await _conponTypeService.GetRootConponTypeAsync();

            List<SelectListItem> rootConponTypeList = new List<SelectListItem>();

            //rootConponTypeList = rootConponType.Select(x => new SelectListItem { Text = x.TypeName, Value = x.Id.ToString() }).ToList();
            rootConponTypeList.Add(new SelectListItem { Text = "根模块", Value = "0" });

            ViewBag.TypeParentId_dd = rootConponTypeList;
        }

    }
}