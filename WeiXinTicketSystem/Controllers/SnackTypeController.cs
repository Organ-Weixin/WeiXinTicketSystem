using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.SnackType;
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
using WeiXinTicketSystem.Properties;
using System.Web;

namespace WeiXinTicketSystem.Controllers
{
    public class SnackTypeController : RootExraController
    {
        private SnackTypeService _snacksTypeService;
        private CinemaService _cinemaService;
        #region ctor
        public SnackTypeController()
        {
            _snacksTypeService = new SnackTypeService();
            _cinemaService = new CinemaService();
        }
        #endregion

        /// <summary>
        /// 套餐类型首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "SnackType").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 套餐类型列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var snacksTypes = await _snacksTypeService.GetSnacksTypePagedAsync(CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search);

            return DynatableResult(snacksTypes.ToDynatableModel(
                snacksTypes.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加套餐类型
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateSnackTypeViewModel model = new CreateOrUpdateSnackTypeViewModel();

            await PreparyCreateOrEditViewData();

            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改套餐类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var snackstype = await _snacksTypeService.GetAsync(id);
            if (snackstype == null)
            {
                return HttpBadRequest();
            }

            CreateOrUpdateSnackTypeViewModel model = new CreateOrUpdateSnackTypeViewModel();

            model.MapFrom(snackstype);

            await PreparyCreateOrEditViewData();

            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改套餐类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateSnackTypeViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改套餐类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateSnackTypeViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            SnackTypeEntity type = new SnackTypeEntity();
            if (model.Id > 0)
            {
                type = await _snacksTypeService.GetAsync(model.Id);
            }

            type.MapFrom(model);
            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                if (type.Image != null && System.IO.File.Exists(rootPath + type.Image))
                {
                    System.IO.File.Delete(rootPath + type.Image);
                }
                string savePath = @"upload\SnackTypeImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                type.Image = savePath + fileName;
            }

            if (type.Id == 0)
            {
                type.IsDel = false;
                await _snacksTypeService.InsertAsync(type);
            }
            else
            {
                await _snacksTypeService.UpdateAsync(type);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "SnackType").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var type = await _snacksTypeService.GetAsync(id);

            if (type != null)
            {
                type.IsDel = true;
                await _snacksTypeService.UpdateAsync(type);
            }

            return Object();
        }

        /// <summary>
        /// 添加或修改角色时，权限选择下拉框数据
        /// </summary>
        /// <returns></returns>
        private async Task PreparyCreateOrEditViewData()
        {
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity>();
                cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
                ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.CinemaName, Value = x.CinemaCode });
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