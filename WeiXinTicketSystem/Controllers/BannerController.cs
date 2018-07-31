using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Banner;
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
    public class BannerController : RootExraController
    {
        private BannerService _BannerService;
        private CinemaService _cinemaService;
        #region ctor
        public BannerController()
        {
            _BannerService = new BannerService();
            _cinemaService = new CinemaService();
        }
        #endregion

        /// <summary>
        /// 图片上传管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Banner").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 图片上传列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<BannerQueryModel> pageModel)
        {
            var paySettings = await _BannerService.GetBannerPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(paySettings.ToDynatableModel(paySettings.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加图片上传
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateBannerViewModel model = new CreateOrUpdateBannerViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改图片上传
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var Banner = await _BannerService.GetBannerByIdAsync(id);
            if (Banner == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateBannerViewModel model = new CreateOrUpdateBannerViewModel();
            model.MapFrom(Banner);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改图片上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateBannerViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改图片上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateBannerViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            BannerEntity Banner = new BannerEntity();
            if (model.Id > 0)
            {
                Banner = await _BannerService.GetBannerByIdAsync(model.Id);
            }

            Banner.MapFrom(model);

            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
                string savePath = @"upload\BannerImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string accessPath = "upload/BannerImg/" + DateTime.Now.ToString("yyyyMM") + "/";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                //判断原图片是否存在
                if (!string.IsNullOrEmpty(Banner.Image))
                {
                    string file = Banner.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                Banner.Image = basePath + accessPath + fileName;
            }

            if (Banner.Id == 0)
            {
                Banner.Created = DateTime.Now;
                await _BannerService.InsertAsync(Banner);
            }
            else
            {
                await _BannerService.UpdateAsync(Banner);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Banner").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除图片上传
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var Banner = await _BannerService.GetBannerByIdAsync(id);

            if (Banner != null)
            {
                Banner.IsDel = true;
                await _BannerService.UpdateAsync(Banner);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定是否启用枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<YesOrNoEnum>();
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
        }


    }
}