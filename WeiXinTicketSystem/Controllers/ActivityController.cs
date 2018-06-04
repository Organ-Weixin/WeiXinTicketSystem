using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Activity;
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
using WeiXin.Tools;
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem.Controllers
{
    public class ActivityController : RootExraController
    {
        private ActivityService _activityService;

        #region ctor
        public ActivityController()
        {
            _activityService = new ActivityService();
        }
        #endregion

        /// <summary>
        /// 活动表管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Activity").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 活动表列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<ActivityQueryModel> pageModel)
        {
            var paySettings = await _activityService.GetActivityPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加活动表
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateActivityViewModel model = new CreateOrUpdateActivityViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改影院支付方式配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateActivityViewModel model = new CreateOrUpdateActivityViewModel();
            model.MapFrom(activity);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateActivityViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }


        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateActivityViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            ActivityEntity activity = new ActivityEntity();
            if (model.Id > 0)
            {
                activity = await _activityService.GetActivityByIdAsync(model.Id);
            }


            activity.MapFrom(model);



            if (activity.Id == 0)
            {
                //图片处理
                if (Image != null)
                {
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\ActivityImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    activity.Image = savePath + fileName;
                }

                await _activityService.InsertAsync(activity);
            }
            else
            {
                string file = Server.MapPath("~/") + activity.Image;
                if (!string.IsNullOrEmpty(file))
                {
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }

                //图片处理
                if (Image != null)
                {
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\ActivityImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    activity.Image = savePath + fileName;
                }

                await _activityService.UpdateAsync(activity);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Activity").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除影院支付方式配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);

            if (activity != null)
            {
                activity.IsDel = true;
                await _activityService.UpdateAsync(activity);
            }
            return Object();
        }

        private void PreparyCreateOrEditViewData()
        {
            //绑定是否启用枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }

    }
}