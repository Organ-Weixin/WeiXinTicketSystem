using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Gift;
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
    public class GiftController : RootExraController
    {
        private GiftService _giftService;

        #region ctor
        public GiftController()
        {
            _giftService = new GiftService();
        }
        #endregion

        /// <summary>
        /// 赠品管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Gift").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        ///// <summary>
        ///// 赠品列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<GiftQueryModel> pageModel)
        {
            var gift = await _giftService.GetGiftPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.Title,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(gift.ToDynatableModel(gift.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加赠品
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateGiftViewModel model = new CreateOrUpdateGiftViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 修改赠品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var gift = await _giftService.GetGiftByIdAsync(id);
            if (gift == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateGiftViewModel model = new CreateOrUpdateGiftViewModel();
            model.MapFrom(gift);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改赠品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateGiftViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }


        /// <summary>
        /// 添加或修改赠品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateGiftViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

           GiftEntity gift = new GiftEntity();
            if (model.Id > 0)
            {
                gift = await _giftService.GetGiftByIdAsync(model.Id);
            }


            gift.MapFrom(model);



            if (gift.Id == 0)
            {
                //图片处理
                if (Image != null)
                {
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\GiftImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    gift.Image = savePath + fileName;
                }

                await _giftService.InsertAsync(gift);
            }
            else
            {
                //图片处理
                if (Image != null)
                {
                    string file = Server.MapPath("~/") + gift.Image;
                    if (!string.IsNullOrEmpty(file))
                    {
                        if (System.IO.File.Exists(file))
                        {
                            //如果存在则删除
                            System.IO.File.Delete(file);
                        }
                    }
                    string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                    string savePath = @"upload\GiftImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                    System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                    string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                    gift.Image = savePath + fileName;
                }

                await _giftService.UpdateAsync(gift);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Gift").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var gift = await _giftService.GetGiftByIdAsync(id);

            if (gift != null)
            {
                gift.IsDel = true;
                await _giftService.UpdateAsync(gift);
            }
            return Object();
        }

        private void PreparyCreateOrEditViewData()
        {
            //绑定是否上架枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }

    }
}