using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.ActivityPopup;
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
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Xml.Linq;
using WeiXinTicketSystem.Properties;
using System.Configuration;

namespace WeiXinTicketSystem.Controllers
{
    public class ActivityPopupController : RootExraController
    {
        private ActivityPopupService _activityPopupService;
        private CinemaService _cinemaService;
        private RecommendGradeService _recommendGradeService;

        #region ctor
        public ActivityPopupController()
        {
            _activityPopupService = new ActivityPopupService();
            _cinemaService = new CinemaService();
            _recommendGradeService = new RecommendGradeService();
        }
        #endregion


        /// <summary>
        /// 活动弹窗管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "ActivityPopup").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();

        }

        ///// <summary>
        ///// 活动弹窗列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<ActivityPopupQueryModel> pageModel)
        {
            var activityPopups = await _activityPopupService.GetActivityPopupPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(activityPopups.ToDynatableModel(activityPopups.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加活动弹窗
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateActivityPopupViewModel model = new CreateOrUpdateActivityPopupViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改活动弹窗
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var activity = await _activityPopupService.GetActivityPopupByIdAsync(id);
            if (activity == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateActivityPopupViewModel model = new CreateOrUpdateActivityPopupViewModel();
            model.MapFrom(activity);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改活动弹窗
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateActivityPopupViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改活动弹窗
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateActivityPopupViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            ActivityPopupEntity activityPopup = new ActivityPopupEntity();
            if (model.Id > 0)
            {
                activityPopup = await _activityPopupService.GetActivityPopupByIdAsync(model.Id);
            }

            activityPopup.MapFrom(model);

            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
                string savePath = @"upload\ActivityPopupImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string accessPath = "upload/ActivityPopupImg/" + DateTime.Now.ToString("yyyyMM") + "/";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                //判断原图片是否存在
                if (!string.IsNullOrEmpty(activityPopup.Image))
                {
                    string file = activityPopup.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                activityPopup.Image = basePath + accessPath + fileName;
            }

            if (activityPopup.Id == 0)
            {
                await _activityPopupService.InsertAsync(activityPopup);
            }
            else
            {
                await _activityPopupService.UpdateAsync(activityPopup);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "ActivityPopup").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除活动弹窗
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var activityPopup = await _activityPopupService.GetActivityPopupByIdAsync(id);

            if (activityPopup != null)
            {
                activityPopup.IsDel = true;
                await _activityPopupService.UpdateAsync(activityPopup);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定弹窗类枚举
            ViewBag.Popup_dd = EnumUtil.GetSelectList<ActivityPopupEnum>();
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

            //推荐等级下拉框
            List<RecommendGradeEntity> recommendGrade = new List<RecommendGradeEntity>();
            recommendGrade.AddRange(await _recommendGradeService.GetAllRecommendGradeAsync());
            ViewBag.GradeCode_dd = recommendGrade.Select(x => new SelectListItem { Text = x.GradeName, Value = x.GradeCode });
        }

    }
}