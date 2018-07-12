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

        #region ctor
        public ConponController()
        {
            _conponService = new ConponService();
            _ticketUsersService = new TicketUsersService();
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
        public async Task<ActionResult> List(DynatablePageModel<ConponQueryModel> pageModel)
        {
            var conpon = await _conponService.GetConponPagedAsync(
                 CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.ConponCode,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(conpon.ToDynatableModel(conpon.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
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
        /// 修改优惠券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var conpon = await _conponService.GetConponByIdAsync(id);
            if (conpon == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateConponViewModel model = new CreateOrUpdateConponViewModel();
            model.MapFrom(conpon);
            await PreparyCreateOrEditViewData();
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

            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
                string savePath = @"upload\ConponImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string accessPath = "upload/ConponImg/" + DateTime.Now.ToString("yyyyMM") + "/";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                //判断原图片是否存在
                if (!string.IsNullOrEmpty(conpon.Image))
                {
                    string file = conpon.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                conpon.Image = basePath + accessPath + fileName;
            }

            if (conpon.Id == 0)
            {
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
            return View(nameof(Index));
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


        private async Task PreparyCreateOrEditViewData()
        {
            //绑定优惠券类型枚举
            ViewBag.ConponType_dd = EnumUtil.GetSelectList<ConponTypeEnum>();

            //绑定是否使用枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<ConponStatusEnum>();


            //绑定用户列表
            List<TicketUserEntity> ticketUsers = new List<TicketUserEntity>();
            ticketUsers.AddRange(await _ticketUsersService.GetAllTicketUserAsync());
            ViewBag.OpenID_dd = ticketUsers.Select(x => new SelectListItem { Text = x.NickName, Value = x.OpenID });

        }

    }
}