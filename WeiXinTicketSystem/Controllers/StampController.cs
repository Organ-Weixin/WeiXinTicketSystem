using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Stamp;
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
    public class StampController : RootExraController
    {
        private StampService _stampService;

        #region ctor
        public StampController()
        {
            _stampService = new StampService();
        }
        #endregion

        /// <summary>
        /// 印章管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Stamp").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 印章列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<StampQueryModel> pageModel)
        {
            var stamp = await _stampService.GetStampPagedAsync(
                pageModel.Query.StampCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(stamp.ToDynatableModel(stamp.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加印章
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateStampViewModel model = new CreateOrUpdateStampViewModel();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改印章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var stamp = await _stampService.GetStampByIdAsync(id);
            if (stamp == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateStampViewModel model = new CreateOrUpdateStampViewModel();
            model.MapFrom(stamp);
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 添加或修改印章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateStampViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }


        /// <summary>
        /// 添加或修改印章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateStampViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            StampEntity stamp = new StampEntity();
            if (model.Id > 0)
            {
                stamp = await _stampService.GetStampByIdAsync(model.Id);
            }

            stamp.MapFrom(model);
            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
                string savePath = @"upload\StampImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string accessPath = "upload/StampImg/" + DateTime.Now.ToString("yyyyMM") + "/";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                //判断原图片是否存在
                if (!string.IsNullOrEmpty(stamp.Image))
                {
                    string file = stamp.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                stamp.Image = basePath + accessPath + fileName;
            }

            if (stamp.Id == 0)
            {
                stamp.StampCode = RandomHelper.CreateRandomCode();
                stamp.Created = DateTime.Now;
                await _stampService.InsertAsync(stamp);
            }
            else
            {
                stamp.Updated = DateTime.Now;
                await _stampService.UpdateAsync(stamp);
            }

            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Stamp").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除印章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var stamp = await _stampService.GetStampByIdAsync(id);

            if (stamp != null)
            {
                stamp.Deleted = true;
                stamp.Updated = DateTime.Now;
                await _stampService.UpdateAsync(stamp);
            }
            return Object();
        }

    }
}