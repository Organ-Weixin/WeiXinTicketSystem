using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Conpon;
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
using System.Configuration;

namespace WeiXinTicketSystem.Controllers
{
    public class ConponController : RootExraController
    {
        private ConponService _conponService;
        private TicketUsersService _ticketUsersService;
        private CinemaService _cinemaService;
        private ConponTypeService _conponTypeService;
        private SnackService _snackService;
        private SessionInfoService _sessionInfoService;
        private ConponGroupService _conponGroupService;
        #region ctor
        public ConponController()
        {
            _conponService = new ConponService();
            _ticketUsersService = new TicketUsersService();
            _cinemaService = new CinemaService();
            _conponTypeService = new ConponTypeService();
            _snackService = new SnackService();
            _sessionInfoService = new SessionInfoService();
            _conponGroupService = new ConponGroupService();
        }
        #endregion


        /// <summary>
        /// 优惠券管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Conpon").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 优惠券列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<ConponQueryModel> pageModel,string CinemaCode,string GroupCode)
        {
            var conpon = await _conponService.GetConponPagedAsync(
                 CinemaCode,
                pageModel.Query.ConponCode,
                GroupCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(conpon.ToDynatableModel(conpon.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        ///// <summary>
        ///// 优惠券组列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> GroupList(DynatablePageModel<ConponQueryModel> pageModel)
        {
            var conpon = await _conponGroupService.GetConponGroupPagedAsync(
                 CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                 pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(conpon.ToDynatableModel(conpon.TotalCount, pageModel.Offset, x => x.ToDynatableItemGroup()));
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateConponViewModel model = new CreateOrUpdateConponViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 生成优惠券
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GenerateCoupon()
        {
            GenerateCouponViewModel model = new GenerateCouponViewModel();
            await PreparyCreateOrEditViewData();
            return View(nameof(GenerateCoupon), model);
        }


        /// <summary>
        /// 券码管理
        /// </summary>
        /// <returns></returns>
        public  ActionResult CouponIndex(string CinemaCode,string GroupCode)
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Conpon").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;

            ViewBag.CinemaCode = CinemaCode;
            ViewBag.GroupCode = GroupCode;
            return View(nameof(CouponIndex));
        }



        /// <summary>
        /// 修改优惠券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id, string CinemaCode, string GroupCode)
        {
            var conpon = await _conponService.GetConponByIdAsync(id);
            if (conpon == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateConponViewModel model = new CreateOrUpdateConponViewModel();
            model.MapFrom(conpon);
            await PreparyCreateOrEditViewData();

            ////绑定优惠券类型SnackCode_dd
            //List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
            //if (model.ConponTypeParentId != null)
            //{
            //    conponTypes.AddRange(await _conponTypeService.GetConponTypeByParentIdAsync(int.Parse(model.ConponTypeParentId)));
            //    ViewBag.ConponTypeCode_dd = conponTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.TypeCode });
            //}

            //绑定套餐
            string typeCode = model.TypeCode;
            if (typeCode == "01" || typeCode == "07")
            {
                List<SnackEntity> snacks = new List<SnackEntity>();
                if (!string.IsNullOrEmpty(model.CinemaCode))
                {
                    snacks.AddRange(_snackService.GetSnacksByCinemaCodeAndStatus(model.CinemaCode, SnackStatusEnum.On));
                    ViewBag.SnackCode_dd = snacks.Select(x => new SelectListItem { Text = x.SnackName, Value = x.SnackCode });
                }
            }
            else if (typeCode == "02" || typeCode == "04")
            {
                List<SessionInfoEntity> sessionFilm = new List<SessionInfoEntity>();
                if (!string.IsNullOrEmpty(model.CinemaCode))
                {
                    sessionFilm.AddRange(_sessionInfoService.GetSessionsFilm(model.CinemaCode, 12, DateTime.Now.AddDays(-3), DateTime.Now));
                    ViewBag.SnackCode_dd = sessionFilm.Select(x => new SelectListItem { Text = x.FilmName, Value = x.FilmName });
                }
            }


            ViewBag.CinemaCode = CinemaCode;
            ViewBag.GroupCode = GroupCode;
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 添加或修改优惠券
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateConponViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改优惠券
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateConponViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            ConponEntity conpon = new ConponEntity();
            if (model.Id > 0)
            {
                conpon = await _conponService.GetConponByIdAsync(model.Id);
            }

            conpon.MapFrom(model);

            //////图片处理
            ////if (Image != null)
            ////{
            ////    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
            ////    string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
            ////    string savePath = @"upload\ConponImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
            ////    string accessPath = "upload/ConponImg/" + DateTime.Now.ToString("yyyyMM") + "/";
            ////    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
            ////    //////判断原图片是否存在
            ////    ////if (!string.IsNullOrEmpty(conpon.Image))
            ////    ////{
            ////    ////    string file = conpon.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
            ////    ////    if (System.IO.File.Exists(file))
            ////    ////    {
            ////    ////        //如果存在则删除
            ////    ////        System.IO.File.Delete(file);
            ////    ////    }
            ////    ////}
            ////    ////string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
            ////    ////conpon.Image = basePath + accessPath + fileName;
            ////}

            if (conpon.Id == 0)
            {
                conpon.ConponCode = RandomHelper.CreateRandomCode();
                conpon.Created = DateTime.Now;
                await _conponService.InsertAsync(conpon);
            }
            else
            {
                conpon.Updated = DateTime.Now;
                await _conponService.UpdateAsync(conpon);
            }

            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Conpon").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return Redirect(Url.Action(nameof(CouponIndex)) + "?CinemaCode=" + conpon.CinemaCode + "&GroupCode=" + conpon.GroupCode);
        }


        /// <summary>
        /// 生成优惠券
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _GenerateCoupon(GenerateCouponViewModel model, HttpPostedFileBase Image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return ErrorObject(string.Join("/n", errorMessages));
                }
                ConponViewEntity ConponGroupView = new ConponViewEntity();
                //生成优惠券
                string strCode = model.TypeCode + RandomHelper.CreateRandomNum(6);
                //生成优惠券组
                ConponGroupEntity conponGroup = new ConponGroupEntity();
                conponGroup.CinemaCode = model.CinemaCode;
                conponGroup.TypeCode = model.TypeCode;
                conponGroup.GroupCode = strCode;
                conponGroup.GroupName = model.Title;
                conponGroup.Price = model.Price;
                conponGroup.ConponNumber = int.Parse(model.GenerateNum.ToString());
                conponGroup.SnackOrFilmCode = model.SnackCode;
                if (!string.IsNullOrEmpty(model.ValidityDate))
                {
                    conponGroup.ValidityDate = DateTime.Parse(model.ValidityDate);
                }
                conponGroup.Remark = model.Remark;
                conponGroup.Created = DateTime.Now;
                ConponGroupView.ConponGroupInfo = conponGroup;

                //生成优惠券
                List<ConponEntity> Conpons = new List<ConponEntity>();
                ConponEntity conponNew;
                for (int i = 0; i < model.GenerateNum; i++)
                {
                    conponNew = new ConponEntity();
                    conponNew.CinemaCode = model.CinemaCode;
                    conponNew.TypeCode = model.TypeCode;
                    conponNew.SnackCode = model.SnackCode;
                    conponNew.Title = model.Title;
                    conponNew.Price = model.Price;
                    conponNew.GroupCode = strCode;
                    if (!string.IsNullOrEmpty(model.ValidityDate))
                    {
                        conponNew.ValidityDate = DateTime.Parse(model.ValidityDate);
                    }
                    conponNew.ConponCode = RandomHelper.GenerateRandomNumber(8);
                    conponNew.Created = DateTime.Now;
                    conponNew.Status = ConponStatusEnum.NotUsed;
                    conponNew.Remark = model.Remark;

                    Conpons.Add(conponNew);
                }
                ConponGroupView.ConponGroupConpons = Conpons;

                _conponGroupService.Insert(ConponGroupView);
                
                var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Conpon").SingleOrDefault();
                List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
                ViewBag.CurrentPermissions = CurrentPermissions;
                return View(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 删除优惠券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var conpon = await _conponService.GetConponByIdAsync(id);

            if (conpon != null)
            {
                conpon.Deleted = true;
                conpon.Updated = DateTime.Now;
                await _conponService.UpdateAsync(conpon);
            }
            return Object();
        }

        /// <summary>
        /// 删除优惠券组和组下面所有优惠券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteGroup(int id,string CinemaCode, string GroupCode)
        {
            try
            {
                var conponGroup = await _conponGroupService.GetConponGroupByIdAsync(id);

                if (conponGroup != null)
                {
                    await _conponGroupService.DeleteAsync(conponGroup);

                    //删除优惠券
                    List<ConponEntity> conpons = new List<ConponEntity>();
                    IList<ConponEntity> iconpons = _conponService.GetConponByCinemaCodeAndGroupCode(CinemaCode, GroupCode);
                    conpons.AddRange(iconpons);
                    foreach (ConponEntity conpon in conpons)
                    {
                        conpon.Deleted = true;
                        conpon.Updated = DateTime.Now;
                        await _conponService.UpdateAsync(conpon);
                    }
                }
                return Object();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task PreparyCreateOrEditViewData()
        {
            //绑定上级优惠券类型下拉框
            List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
            conponTypes.AddRange(await _conponTypeService.GetRootConponTypeAsync());
            ViewBag.TypeCode_dd = conponTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.TypeCode });

            //绑定是否使用枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<ConponStatusEnum>();


            //绑定用户列表
            List<TicketUserEntity> ticketUsers = new List<TicketUserEntity>();
            ticketUsers.AddRange(await _ticketUsersService.GetAllTicketUserAsync());
            ViewBag.OpenID_dd = ticketUsers.Select(x => new SelectListItem { Text = x.NickName, Value = x.OpenID });

            //影院下拉
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

            //优惠券类型下拉框
            //ViewBag.ConponTypeCode_dd = new List<SelectListItem>();

            //套餐下拉框
            ViewBag.SnackCode_dd = new List<SelectListItem>();

        }

        ///// <summary>
        ///// 绑定优惠券类型
        ///// </summary>
        ///// <param name="typeParentId"></param>
        ///// <returns></returns>
        //public async Task<ActionResult> GetConponType(int typeParentId)
        //{
        //    List<ConponTypeEntity> conponTypes = new List<ConponTypeEntity>();
        //    IList<ConponTypeEntity> iconponTypes = await _conponTypeService.GetConponTypeByParentIdAsync(typeParentId);
        //    conponTypes.AddRange(iconponTypes);
        //    string jsonresult = JSONHelper.ToJson(conponTypes);
        //    return Json(jsonresult, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 绑定套餐
        /// </summary>
        /// <param name="typeParentId"></param>
        /// <returns></returns>
        public ActionResult GetSnackCode(string cinemaCode)
        {
            List<SnackEntity> snacks = new List<SnackEntity>();
            IList<SnackEntity> isnacksv =  _snackService.GetSnacksByCinemaCodeAndStatus(cinemaCode, SnackStatusEnum.On);
            snacks.AddRange(isnacksv);
            string jsonresult = JSONHelper.ToJson(snacks);
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 绑定影片
        /// </summary>
        /// <param name="typeParentId"></param>
        /// <returns></returns>
        public ActionResult GetSessinsFilm(string cinemaCode)
        {
            List<SessionInfoEntity> sessions = new List<SessionInfoEntity>();
            IList<SessionInfoEntity> isessions = _sessionInfoService.GetSessionsFilm(cinemaCode,12,DateTime.Now.AddDays(-3),DateTime.Now);
            //var iisessions = isessions.GroupBy(x => x.FilmName);
            sessions.AddRange(isessions);
            string jsonresult = JSONHelper.ToJson(sessions);
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

    }

}