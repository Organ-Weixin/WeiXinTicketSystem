using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.MemberChargeSetting;
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
    public class MemberChargeSettingController : RootExraController
    {
        private MemberChargeSettingService _memberChargeSettingService;
        private CinemaService _cinemaService;
        private ConponTypeService _conponTypeService;
        private ConponGroupService _conponGroupService;

        #region ctor
        public MemberChargeSettingController()
        {
            _memberChargeSettingService = new MemberChargeSettingService();
            _cinemaService = new CinemaService();
            _conponTypeService = new ConponTypeService();
            _conponGroupService = new ConponGroupService();
        }
        #endregion

        /// <summary>
        /// 会员卡充值赠送条件管理首页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "MemberChargeSetting").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            await PreparyCreateOrEditViewData();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 会员卡充值赠送条件列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<MemberChargeSettingQueryModel> pageModel)
        {
            var memberChargeSetting = await _memberChargeSettingService.GetMemberChargeSettingPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode_dd : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(memberChargeSetting.ToDynatableModel(memberChargeSetting.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加会员卡充值赠送条件
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateMemberChargeSettingViewModel model = new CreateOrUpdateMemberChargeSettingViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改会员卡充值赠送条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var memberChargeSetting = await _memberChargeSettingService.GetMemberChargeSettingByIdAsync(id);
            if (memberChargeSetting == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateMemberChargeSettingViewModel model = new CreateOrUpdateMemberChargeSettingViewModel();
            model.MapFrom(memberChargeSetting);
            await PreparyCreateOrEditViewData();

            //绑定优惠券类型
            List<ConponGroupEntity> conponGroups = new List<ConponGroupEntity>();
            conponGroups.AddRange(_conponGroupService.GetConponGroupByCinemaCodeAndTypeCode(model.CinemaCode, model.TypeCode));
            ViewBag.GroupCode_dd = conponGroups.Select(x => new SelectListItem { Text = x.GroupName, Value = x.GroupCode });
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改会员卡充值赠送条件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateMemberChargeSettingViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改会员卡充值赠送条件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateMemberChargeSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            MemberChargeSettingEntity memberChargeSetting = new MemberChargeSettingEntity();
            //decimal? ylPrice = 0;
            if (model.Id > 0)
            {
                memberChargeSetting = await _memberChargeSettingService.GetMemberChargeSettingByIdAsync(model.Id);
                //ylPrice = memberChargeSetting.Price;
            }

            memberChargeSetting.MapFrom(model);
            //备注
            ConponGroupEntity conponGroup = _conponGroupService.GetConponGroupByCinemaCodeAndGroupCode(model.CinemaCode, model.GroupCode);
            memberChargeSetting.Remark = conponGroup.Remark;

            if (memberChargeSetting.Id == 0)
            {
                //IList<MemberChargeSettingEntity> memberChargeSettingPrice = await _memberChargeSettingService.GetMemberChargeSettingByCinemaCodeAndPriceAsync(model.CinemaCode, model.Price);
                //if (memberChargeSettingPrice.Count > 0 )
                //{
                //    return ErrorObject("充值金额已存在！请重新填写！");
                //}
                memberChargeSetting.Created = DateTime.Now;
                await _memberChargeSettingService.InsertAsync(memberChargeSetting);
            }
            else
            {
                //if (ylPrice != model.Price)
                //{
                //    IList<MemberChargeSettingEntity> memberChargeSettingPrice = await _memberChargeSettingService.GetMemberChargeSettingByCinemaCodeAndPriceAsync(model.CinemaCode, model.Price);
                //    if (memberChargeSettingPrice.Count > 0)
                //    {
                //        return ErrorObject("充值金额已存在！请重新填写！");
                //    }
                //}
                memberChargeSetting.Updated = DateTime.Now;
                await _memberChargeSettingService.UpdateAsync(memberChargeSetting);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除会员卡充值赠送条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var memberChargeSetting = await _memberChargeSettingService.GetMemberChargeSettingByIdAsync(id);

            if (memberChargeSetting != null)
            {
                memberChargeSetting.Deleted = true;
                await _memberChargeSettingService.UpdateAsync(memberChargeSetting);
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
            ViewBag.TypeCode_dd = conponTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.TypeCode });

            //优惠券类型下拉框
            ViewBag.GroupCode_dd = new List<SelectListItem>();

        }

        /// <summary>
        /// 绑定优惠券类型
        /// </summary>
        /// <param name="typeParentId"></param>
        /// <returns></returns>
        public ActionResult GetConponType(string cinemaCode, string typeCode)
        {
            List<ConponGroupEntity> conponGroups = new List<ConponGroupEntity>();
            IList<ConponGroupEntity> iconponGroups = _conponGroupService.GetConponGroupByCinemaCodeAndTypeCode(cinemaCode, typeCode);
            conponGroups.AddRange(iconponGroups);
            string jsonresult = JSONHelper.ToJson(conponGroups);
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

    }
}