using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.GivingConditions;
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
    public class GivingConditionsController : RootExraController
    {
        private GivingConditionsService _givingConditionsService;
        private CinemaService _cinemaService;
        private ConponTypeService _conponTypeService;

        #region ctor
        public GivingConditionsController()
        {
            _givingConditionsService = new GivingConditionsService();
            _cinemaService = new CinemaService();
            _conponTypeService = new ConponTypeService();
        }
        #endregion


        /// <summary>
        /// 赠送条件管理首页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "GivingConditions").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            await PreparyCreateOrEditViewData();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 赠送条件列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<GivingConditionsQueryModel> pageModel)
        {
            var givingConditions = await _givingConditionsService.GetGivingConditionPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode_dd : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(givingConditions.ToDynatableModel(givingConditions.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加赠送条件
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateGivingConditionsViewModel model = new CreateOrUpdateGivingConditionsViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改赠送条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var givingConditions = await _givingConditionsService.GetGivingConditionByIdAsync(id);
            if (givingConditions == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateGivingConditionsViewModel model = new CreateOrUpdateGivingConditionsViewModel();
            model.MapFrom(givingConditions);
            await PreparyCreateOrEditViewData();

            //绑定优惠券类型
            List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
            if (model.ConponTypeParentId != null)
            {
                conponTypes.AddRange(await _conponTypeService.GetConponTypeByParentIdAsync(int.Parse(model.ConponTypeParentId)));
                ViewBag.ConponTypeCode_dd = conponTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.TypeCode });
            }
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改赠送条件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateGivingConditionsViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改赠送条件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateGivingConditionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            GivingConditionEntity givingConditions = new GivingConditionEntity();
            if (model.Id > 0)
            {
                givingConditions = await _givingConditionsService.GetGivingConditionByIdAsync(model.Id);
            }

            givingConditions.MapFrom(model);

            if (givingConditions.Id == 0)
            {
                givingConditions.Created = DateTime.Now;
                await _givingConditionsService.InsertAsync(givingConditions);
            }
            else
            {
                givingConditions.Updated = DateTime.Now;
                await _givingConditionsService.UpdateAsync(givingConditions);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除赠送条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var givingConditions = await _givingConditionsService.GetGivingConditionByIdAsync(id);

            if (givingConditions != null)
            {
                givingConditions.Deleted = true;
                await _givingConditionsService.UpdateAsync(givingConditions);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity>();
                cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
                ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.Name, Value = x.Code });
            }
            else
            {
                ViewBag.CinemaCode_dd = new List<SelectListItem>
                {
                    new SelectListItem { Text = CurrentUser.CinemaName, Value = CurrentUser.CinemaCode }
                };
            }

            //绑定上级优惠券类型下拉框
            List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
            conponTypes.AddRange(await _conponTypeService.GetRootConponTypeAsync());
            ViewBag.ConponTypeParentId_dd = conponTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.Id.ToString() });

            //优惠券类型下拉框
            ViewBag.ConponTypeCode_dd = new List<SelectListItem>();

        }

        /// <summary>
        /// 绑定优惠券类型
        /// </summary>
        /// <param name="typeParentId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetConponType(int typeParentId)
        {
            List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
            IList<ConponTypeEntity> iconponTypes = await _conponTypeService.GetConponTypeByParentIdAsync(typeParentId);
            conponTypes.AddRange(iconponTypes);
            string jsonresult = JSONHelper.ToJson(conponTypes);
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }
    }
}