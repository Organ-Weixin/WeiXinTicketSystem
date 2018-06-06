using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Bnanner;
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
    public class BnannerController : RootExraController
    {
        private BnannerService _bnannerService;

        #region ctor
        public BnannerController()
        {
            _bnannerService = new BnannerService();
        }
        #endregion

        /// <summary>
        /// 图片上传管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Bnanner").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 图片上传列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<BnannerQueryModel> pageModel)
        {
            var paySettings = await _bnannerService.GetBnannerPagedAsync(
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
        public ActionResult Create()
        {
            CreateOrUpdateBnannerViewModel model = new CreateOrUpdateBnannerViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改图片上传
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var bnanner = await _bnannerService.GetBnannerByIdAsync(id);
            if (bnanner == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateBnannerViewModel model = new CreateOrUpdateBnannerViewModel();
            model.MapFrom(bnanner);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改图片上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateBnannerViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改图片上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateBnannerViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            BnannerEntity bnanner = new BnannerEntity();
            if (model.Id > 0)
            {
                bnanner = await _bnannerService.GetBnannerByIdAsync(model.Id);
            }


            bnanner.MapFrom(model);



            if (bnanner.Id == 0)
            {
                //图片处理
                if (Image != null)
                {
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\BnannerImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    bnanner.Image = savePath + fileName;
                    bnanner.Created = DateTime.Now;
                }

                await _bnannerService.InsertAsync(bnanner);
            }
            else
            {
                //图片处理
                if (Image != null)
                {
                    string file = Server.MapPath("~/") + bnanner.Image;
                    if (!string.IsNullOrEmpty(file))
                    {
                        if (System.IO.File.Exists(file))
                        {
                            //如果存在则删除
                            System.IO.File.Delete(file);
                        }
                    }
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\BnannerImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    bnanner.Image = savePath + fileName;
                }

                await _bnannerService.UpdateAsync(bnanner);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Bnanner").SingleOrDefault();
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
            var bnanner = await _bnannerService.GetBnannerByIdAsync(id);

            if (bnanner != null)
            {
                bnanner.IsDel = true;
                await _bnannerService.UpdateAsync(bnanner);
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