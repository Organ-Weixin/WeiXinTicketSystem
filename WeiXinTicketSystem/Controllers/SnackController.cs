using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Snack;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Service;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.IO;

namespace WeiXinTicketSystem.Controllers
{
    public class SnackController : RootExraController
    {

        private SnackService _snackService;
        private CinemaService _cinemaService;
        private SnackTypeService _snacksTypeService;
        #region ctor
        public SnackController()
        {
            _snackService = new SnackService();
            _cinemaService = new CinemaService();
            _snacksTypeService = new SnackTypeService();
        }
        #endregion
        public async Task<ActionResult> Index()
        {
            await PrepareIndexViewData();

            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<SnackPageQueryModel> pageModel)
        {
            var snacks = await _snackService.GetSnacksPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode_dd : CurrentUser.CinemaCode,
                 pageModel.Query.SnackCode,
                 pageModel.Query.TypeId_dd,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search
                 );

            return DynatableResult(snacks.ToDynatableModel(
                snacks.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增套餐
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateSnackViewModel model = new CreateOrUpdateSnackViewModel();

            await PrepareIndexViewData();

            return View(nameof(CreateOrUpdate), model);
        }
        /// <summary>
        /// 编辑套餐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var snack = await _snackService.GetSnackByIdAsync(id);
            if (snack == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateSnackViewModel model = new CreateOrUpdateSnackViewModel();
            model.MapFrom(snack);
            await PrepareIndexViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var snack = await _snackService.GetSnackByIdAsync(id);
            if (snack != null && snack.Id > 0)
            {
                snack.IsDel = true;
                await _snackService.UpdateAsync(snack);
            }
            return Object();
        }

        /// <summary>
        /// 添加或修改套餐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateSnackViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改套餐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateSnackViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            SnackEntity snack = new SnackEntity();
            if (model.Id > 0)
            {
                snack = await _snackService.GetSnackByIdAsync(model.Id);
            }
            snack.MapFrom(model);
            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string savePath = @"upload\SnackImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath,DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                snack.Image = savePath + fileName;
            }
            if (snack.Id == 0)
            {
                //判断是否已经存在
                var existedsnack = await _snackService.GetSnackByCinemaCodeAndNameAsync(model.CinemaCode, model.SnackName);
                if (existedsnack != null)
                {
                    return ErrorObject("套餐已存在！");
                }
                snack.SnackCode = RandomHelper.CreatePwd(8);//生成八位套餐编码
                //snack.Image = ImageByte;
                snack.Status = SnackStatusEnum.On;//0下架，1上架
                snack.IsDel = false;
                snack.IsRecommand = false;
                await _snackService.InsertAsync(snack);
            }
            else
            {
                await _snackService.UpdateAsync(snack);
            }
            //return RedirectObject(Url.Action(nameof(Index)));
            await PrepareIndexViewData();
            return View(nameof(Index));
        }

        /// <summary>
        /// 准备首页下拉框数据
        /// </summary>
        /// <returns></returns>
        private async Task PrepareIndexViewData()
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

            List<SnackTypeEntity> snacksTypes = new List<SnackTypeEntity>();
            snacksTypes.AddRange(await _snacksTypeService.GetAllSnacksTypesAsync(CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? "" : CurrentUser.CinemaCode));
            ViewBag.TypeId_dd = snacksTypes.Select(x => new SelectListItem { Text = x.TypeName, Value = x.Id.ToString() });
        }
    }
}